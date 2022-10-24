using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Newtonsoft.Json;
using System.Security.Cryptography.X509Certificates;

namespace XTC.FMP.MOD.Assloud.App.Service
{
    public class ContentDAO : DAO<ContentEntity>
    {
        public ContentDAO(IMongoDatabase _mongoDatabase) : base(_mongoDatabase, "Content")
        {
        }

        public LIB.Proto.ContentEntity ToProtoEntity(ContentEntity _entity)
        {
            var entity = new LIB.Proto.ContentEntity();
            entity.Uuid = _entity.Uuid.ToString();
            entity.ForeignBundleUuid = _entity.foreign_bundle_uuid.ToString();
            entity.Name = _entity.name;
            foreach (var pair in _entity.kvS)
                entity.KvS[pair.Key] = pair.Value;
            entity.Alias = _entity.alias;
            entity.Title = _entity.title;
            entity.Caption = _entity.caption;
            entity.Label = _entity.label;
            entity.Topic = _entity.topic;
            entity.Description = _entity.description;
            foreach (var pair in _entity.alias_i18nS)
                entity.AliasI18NS[pair.Key] = pair.Value;
            foreach (var pair in _entity.title_i18nS)
                entity.TitleI18NS[pair.Key] = pair.Value;
            foreach (var pair in _entity.caption_i18nS)
                entity.CaptionI18NS[pair.Key] = pair.Value;
            foreach (var pair in _entity.label_i18nS)
                entity.LabelI18NS[pair.Key] = pair.Value;
            foreach (var pair in _entity.topic_i18nS)
                entity.TopicI18NS[pair.Key] = pair.Value;
            foreach (var pair in _entity.description_i18nS)
                entity.DescriptionI18NS[pair.Key] = pair.Value;
            foreach (var label in _entity.labelS)
                entity.LabelS.Add(label);
            foreach (var tag in _entity.tagS)
                entity.TagS.Add(tag);
            entity.ExtraBundleName = "";
            return entity;
        }

        public LIB.Proto.ContentEntity ExtraToProtoEntity(ExtraContentEntity _entity)
        {
            var entity = ToProtoEntity(_entity);
            entity.ExtraBundleName = _entity.extra_bundle_name;
            return entity;
        }



        public virtual async Task<ContentEntity?> FindWithNameAsync(string _name) =>
            await collection_.Find(x => x.name.Equals(_name)).FirstOrDefaultAsync();

        public virtual async Task<long> CountAsync(string _bundleUUID) =>
            await collection_.CountDocumentsAsync(x => x.foreign_bundle_uuid.Equals(_bundleUUID));

        /// <summary>
        /// 异步搜索实体
        /// </summary>
        /// <param name="_offset">偏移量</param>
        /// <param name="_count">查询量</param>
        /// <returns></returns>
        public virtual async Task<KeyValuePair<long, List<ContentEntity>>> SearchAsync(int _offset, int _count, string? _name, string[]? _labels, string[]? _tags)
        {
            // _name, _labels, _tags 三个条件至少需要满足一个
            if (string.IsNullOrWhiteSpace(_name) && (null == _labels || _labels.Length == 0) && (null == _tags || _tags.Length == 0))
                return new KeyValuePair<long, List<ContentEntity>>(0, new List<ContentEntity>());

            System.Func<string[], string[], bool> hasSubSet = (_val, _sub) =>
            {
                foreach (var sub in _sub)
                {
                    foreach (var val in _val)
                    {
                        if (!val.Equals(sub))
                            return false;
                    }
                }
                return true;
            };

            var filter = Builders<ContentEntity>.Filter.Where(x =>
                (string.IsNullOrWhiteSpace(_name) || (null != x.name && x.name.ToLower().Contains(_name.ToLower()))) &&
                (null == _labels || hasSubSet(x.labelS, _labels)) &&
                (null == _tags || hasSubSet(x.tagS, _tags))
            );

            var found = collection_.Find(filter);

            var total = await found.CountDocumentsAsync();
            var bundles = await found.Skip((int)_offset).Limit((int)_count).ToListAsync();

            return new KeyValuePair<long, List<ContentEntity>>(total, bundles);
        }

        public async Task PutContentEntityToMinIO(ContentEntity _entity, MinIOClient _minioClient)
        {
            string filepath = String.Format("{0}/{1}/meta.json", _entity.foreign_bundle_uuid, _entity.Uuid.ToString());
            //将meta存入对象存储引擎中
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(_entity));
            using (MemoryStream stream = new MemoryStream(bytes))
            {
                await _minioClient.PutObject(filepath, stream);
            }
        }

        public async Task RemoveContentEntityFromMinIO(ContentEntity _entity, MinIOClient _minioClient)
        {
            string filepath = String.Format("{0}/{1}/meta.json", _entity.foreign_bundle_uuid, _entity.Uuid.ToString());
            await _minioClient.RemoveObject(filepath);
        }

        public virtual async Task<IAsyncCursor<BsonDocument>> AggregateListAsync(int _offset, int _count, string _bundleUUID)
        {
            /*
            db.getCollection("Content").aggregate([
                {
                  $lookup:{from:"Bundle", localField:"foreign_bundle_uuid", foreignField:"Uuid", as:"_extraBundleS"}
                },
                {
                  $replaceRoot: { newRoot: { $mergeObjects: [ "$$ROOT", {_extraBundle:{ $arrayElemAt: [ "$_extraBundleS", 0 ] }}] } }
                },
                {
                  $replaceRoot: { newRoot: { $mergeObjects: [ "$$ROOT", {extra_bundle_name:"$_extraBundle.name"}] } }
                },
                {
                  $project:{_extraBundleS:0, _extraBundle:0}
                }
            ]).pretty()
            */
            BsonDocument? match = null;
            if (!string.IsNullOrEmpty(_bundleUUID))
            {
                match = new BsonDocument {
                {
                    "$match", new BsonDocument {
                        { "foreign_bundle_uuid", Guid.Parse(_bundleUUID)}
                    }
                }
            };
            }
            var lookup = new BsonDocument {
                {
                    "$lookup", new BsonDocument {
                        { "from", "Bundle"},
                        { "localField", "foreign_bundle_uuid"},
                        { "foreignField", "Uuid"},
                        { "as", "_extraBundleS"},
                    }
                }
            };
            var replaceRoot1 = new BsonDocument {
                {
                    "$replaceRoot", new BsonDocument{
                        {
                            "newRoot", new BsonDocument{
                                {
                                    "$mergeObjects", new BsonArray{
                                        "$$ROOT",
                                        new BsonDocument {
                                            {
                                                "_extraBundle", new BsonDocument {
                                                    {
                                                        "$arrayElemAt", new BsonArray {
                                                            "$_extraBundleS",
                                                            0
                                                        }
                                                    }
                                                }
                                            }
                                        },
                                    }
                                }
                             }
                        },
                    }
                }
            };
            var replaceRoot2 = new BsonDocument {
                {
                    "$replaceRoot", new BsonDocument{
                        {
                            "newRoot", new BsonDocument{
                                {
                                    "$mergeObjects", new BsonArray{
                                        "$$ROOT",
                                        new BsonDocument {
                                            { "extra_bundle_name", "$_extraBundle.name" }
                                        },
                                    }
                                }
                             }
                        },
                    }
                }
            };
            var project = new BsonDocument {
                {
                    "$project", new BsonDocument{
                        { "_extraBundleS", 0 },
                        { "_extraBundle", 0 },
                    }
                }
            };
            var skip = new BsonDocument
            {
                { "$skip", _offset }
            };
            var limit = new BsonDocument
            {
                { "$limit", _count}
            };
            List<BsonDocument> pipeline = new List<BsonDocument>();
            if (null != match)
            {
                pipeline.Add(match);
            }
            pipeline.AddRange(new[] { lookup, replaceRoot1, replaceRoot2, project, skip, limit });
            return await collection_.AggregateAsync<BsonDocument>(pipeline);
        }
    }
}

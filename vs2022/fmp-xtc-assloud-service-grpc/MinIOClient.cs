﻿using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel;
using Minio.Exceptions;
using Newtonsoft.Json;
using System.Reactive.Linq;

namespace XTC.FMP.MOD.Assloud.App.Service
{
    public class MinIOClient
    {
        private readonly IOptions<MinIOSettings> settings_;
        private MinioClient client_;
        private MinioClient presignedClient_;

        public MinIOClient(IOptions<MinIOSettings> _settings)
        {
            settings_ = _settings;
            client_ = new MinioClient()
                .WithEndpoint(settings_.Value.Endpoint)
                .WithCredentials(settings_.Value.AccessKey, settings_.Value.SecretKey)
                .Build();
            presignedClient_ = new MinioClient()
                .WithEndpoint(settings_.Value.AddressUrl)
                .WithCredentials(settings_.Value.AccessKey, settings_.Value.SecretKey)
                .Build();
            if (_settings.Value.AddressSSL)
                presignedClient_ = presignedClient_.WithSSL();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_key"></param>
        /// <param name="_expiry">过期时间，单位秒</param>
        /// <returns></returns>
        public async Task<string> PresignedPutObject(string _path, int _expiry)
        {
            PresignedPutObjectArgs args = new PresignedPutObjectArgs()
                                                    .WithBucket(settings_.Value.Bucket)
                                                    .WithObject(_path).WithExpiry(_expiry);
            return await presignedClient_.PresignedPutObjectAsync(args);
        }

        public async Task<KeyValuePair<string, ulong>> StateObject(string _path)
        {
            StatObjectArgs statObjectArgs = new StatObjectArgs()
                                            .WithBucket(settings_.Value.Bucket)
                                            .WithObject(_path);
            string etag = "";
            ulong size = 0;
            try
            {
                ObjectStat objectStat = await client_.StatObjectAsync(statObjectArgs);
                etag = objectStat.ETag;
                size = (ulong)objectStat.Size;
            }
            catch (ObjectNotFoundException)
            {
                etag = "";
                size = 0;
            }
            return new KeyValuePair<string, ulong>(etag, size);
        }

        public async Task PutObject(string _path, Stream _stream)
        {
            PutObjectArgs putObjectArgs = new PutObjectArgs()
                                                        .WithBucket(settings_.Value.Bucket)
                                                        .WithObject(_path)
                                                        .WithStreamData(_stream)
                                                        .WithObjectSize(_stream.Length);
            await client_.PutObjectAsync(putObjectArgs);
        }

        public async Task RemoveObject(string _path)
        {
            RemoveObjectArgs removeObjectArgs = new RemoveObjectArgs()
                                                        .WithBucket(settings_.Value.Bucket)
                                                        .WithObject(_path);
            await client_.RemoveObjectAsync(removeObjectArgs);
        }

        public string GetAddressUrl(string _path)
        {
            string scheme = settings_.Value.AddressSSL ? "https" : "http";
            return string.Format("{0}://{1}/{2}/{3}", scheme, settings_.Value.AddressUrl, settings_.Value.Bucket, _path);
        }

        public async Task GenerateManifestAsync(string? _path)
        {
            if (string.IsNullOrEmpty(_path))
                return;

            ListObjectsArgs listObjectsArgs = new ListObjectsArgs()
                .WithBucket(settings_.Value.Bucket)
                .WithPrefix(_path).WithRecursive(true);
            var observable = client_.ListObjectsAsync(listObjectsArgs);
            var items = await observable.ToList();
            ManifestSchema schema = new ManifestSchema();
            foreach (var item in items)
            {
                if (item.Key.Equals(string.Format("{0}/manifest.json", _path)))
                    continue;
                schema.entries.Add(new ManifestSchema.Entry()
                {
                    file = item.Key,
                    hash = item.ETag,
                    size = item.Size,
                });
            }
            string filepath = String.Format("{0}/manifest.json", _path);
            //将meta存入对象存储引擎中
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(schema));
            using (MemoryStream stream = new MemoryStream(bytes))
            {
                await PutObject(filepath, stream);
            }
        }
    }
}

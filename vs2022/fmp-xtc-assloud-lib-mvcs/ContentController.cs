
using System.Collections.Generic;
using System.Threading;
using XTC.FMP.MOD.Assloud.LIB.Proto;

namespace XTC.FMP.MOD.Assloud.LIB.MVCS
{
    /// <summary>
    /// Content控制层
    /// </summary>
    public class ContentController : ContentControllerBase
    {
        /// <summary>
        /// 完整名称
        /// </summary>
        public const string NAME = "XTC.FMP.MOD.Assloud.LIB.MVCS.ContentController";

        /// <summary>
        /// 带uid参数的构造函数
        /// </summary>
        /// <param name="_uid">实例化后的唯一识别码</param>
        /// <param name="_gid">直系的组的ID</param>
        public ContentController(string _uid, string _gid) : base(_uid, _gid)
        {
        }

        public override void UpdateProtoMatch(ContentModel.ContentStatus? _status, ContentListResponse _response, SynchronizationContext? _context)
        {
            if (null != _status)
            {
                _status.contentMap = new Dictionary<string, ContentEntity>();

                foreach (ContentEntity entity in _response.Contents)
                {
                    _status.contentMap[entity.Meta.Uri] = entity;
                }
            }

            base.UpdateProtoMatch(_status, _response, _context);
        }
    }
}


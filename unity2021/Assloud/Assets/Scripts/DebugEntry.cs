
using UnityEngine;

namespace XTC.FMP.MOD.Assloud.LIB.Unity
{
    /// <summary>
    /// 调试入口
    /// </summary>
    /// <remarks>
    /// 不参与模块编译，仅用于在编辑器中开发调试
    /// </remarks>
    public class DebugEntry : MyEntry 
    {
        /// <summary>
        /// 调试预加载
        /// </summary>
        public void __DebugPreload(GameObject _exportRoot)
        {
            processRoot(_exportRoot);
        }

        /// <summary>
        /// 调试创建
        /// </summary>
        /// <param name="_uid">实例的uid</param>
        /// <param name="_style">实例的样式名</param>
        public void __DebugCreate(string _uid, string _style)
        {
            runtime_.CreateInstance(_uid, _style);
        }

        /// <summary>
        /// 调试打开
        /// </summary>
        /// <param name="_uid">实例的uid</param>
        /// <param name="_source">内容的源的类型</param>
        /// <param name="_uri">内容的地址</param>
        /// <param name="_delay">延迟时间，单位秒</param>
        public void __DebugOpen(string _uid, string _source, string _uri, float _delay)
        {
            runtime_.OpenInstance(_uid, _source, _uri, _delay);
        }

        /// <summary>
        /// 调试关闭
        /// </summary>
        /// <param name="_uid">实例的uid</param>
        /// <param name="_delay">延迟时间，单位秒</param>
        public void __DebugClose(string _uid, float _delay)
        {
            runtime_.CloseInstance(_uid, _delay);
        }

        /// <summary>
        /// 调试删除
        /// </summary>
        /// <param name="_uid">实例的uid</param>
        public void __DebugDelete(string _uid)
        {
            runtime_.DeleteInstance(_uid);
        }
    }
}

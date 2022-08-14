
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using LibMVCS = XTC.FMP.LIB.MVCS;
using XTC.FMP.MOD.Assloud.LIB.Proto;
using XTC.FMP.MOD.Assloud.LIB.MVCS;

namespace XTC.FMP.MOD.Assloud.LIB.Unity
{
    /// <summary>
    /// 实例类
    /// </summary>
    public class MyInstance : MyInstanceBase
    {
        private MyConfig.Style style_ { get; set; }

        /// <summary>
        /// 应用样式
        /// </summary>
        /// <param name="_style">样式</param>
        public void ApplyStyle(MyConfig.Style _style)
        {
            style_ = _style;
        }

        /// <summary>
        /// 当被创建时
        /// </summary>
        public void HandleCreated()
        {
        }

        /// <summary>
        /// 当被删除时
        /// </summary>
        public void HandleDeleted()
        {
        }

        /// <summary>
        /// 当被打开时
        /// </summary>
        public void HandleOpened()
        {
            rootUI.gameObject.SetActive(true);
        }

        /// <summary>
        /// 当被关闭时
        /// </summary>
        public void HandleClosed()
        {
            rootUI.gameObject.SetActive(false);
        }
    }
}

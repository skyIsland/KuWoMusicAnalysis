using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sky.Models
{
    /// <summary>
    /// 酷我音乐歌曲信息
    /// </summary>
    public class KwMusic
    {
        /// <summary>
        /// 歌曲名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 专辑名称
        /// </summary>
        public string AlbumName { get; set; }

        /// <summary>
        /// 歌手名称
        /// </summary>
        public string SingerName { get; set; }

        /// <summary>
        /// 下载地址
        /// </summary>
        public string DlUrl { get; set; }
    }
}

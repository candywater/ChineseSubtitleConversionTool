﻿using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ChineseSubtitleConversionTool
{
    public class OfficeWordConvert
    {
        private _Application appWord;
        private Document doc;

        /// <summary>
        /// 构造函数
        /// </summary>
        public OfficeWordConvert()
        {
            appWord = new Application();
            object template = Missing.Value;
            object newTemplate = Missing.Value;
            object docType = Missing.Value;
            object visible = true;
            doc = appWord.Documents.Add(ref template, ref newTemplate, ref docType, ref visible);
        }

        /// <summary>
        /// 简体转繁体函数
        /// </summary>
        /// <param name="src">简体字符串</param>
        /// <returns>繁体字符串</returns>
        public string Chs2Cht(string src)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string line in src.Split(new string[] { "\n" }, StringSplitOptions.None))
            {
                sb.AppendLine(chs_to_cht(line.Replace("\r", "")));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 繁体转简体函数
        /// </summary>
        /// <param name="src">繁体字符串</param>
        /// <returns>简体字符串</returns>
        public string Cht2Chs(string src)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string line in src.Split(new string[] { "\n" }, StringSplitOptions.None))
            {
                sb.AppendLine(cht_to_chs(line.Replace("\r", "")));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        ~OfficeWordConvert()
        {
            Dispose();
        }

        private void Dispose()
        {
            object saveChange = 0;
            object originalFormat = Missing.Value;
            object routeDocument = Missing.Value;
            appWord.Quit(ref saveChange, ref originalFormat, ref routeDocument);
            doc = null;
            appWord = null;
            GC.Collect();
        }

        /// <summary>
        /// 简体转繁体函数
        /// </summary>
        /// <param name="src">简体字符串</param>
        /// <returns>繁体字符串</returns>
        private string chs_to_cht(string src)
        {
            appWord.Selection.Delete();
            appWord.Selection.TypeText(src);
            appWord.Selection.Range.TCSCConverter(WdTCSCConverterDirection.wdTCSCConverterDirectionSCTC, true, true);
            appWord.ActiveDocument.Select();
            return appWord.Selection.Text;
        }

        /// <summary>
        /// 繁体转简体函数
        /// </summary>
        /// <param name="src">繁体字符串</param>
        /// <returns>简体字符串</returns>
        private string cht_to_chs(string src)
        {
            appWord.Selection.Delete();
            appWord.Selection.TypeText(src);
            appWord.Selection.Range.TCSCConverter(WdTCSCConverterDirection.wdTCSCConverterDirectionTCSC, true, true);
            appWord.ActiveDocument.Select();
            return appWord.Selection.Text;
        }
    }
}

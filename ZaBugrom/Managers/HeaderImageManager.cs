﻿using System;
using System.Collections.Generic;
using BLToolkit.Reflection;
using CommonDAL.SqlDAL;
using Models.Data;

namespace ZaBugrom.Managers
{
    public static class HeaderImageManager
    {
        private static List<HeaderImageData> _headerImageList;
        private static readonly HeaderImageRepository _repository = TypeAccessor<HeaderImageRepository>.CreateInstance();
        private static HeaderImageData _currentHeaderImage;
        private static DateTime _lastCheckTime;

        public static HeaderImageData CurrentHeaderImage
        {
            get
            {
                var now = DateTime.Now;

                if ((now - _lastCheckTime).TotalHours > 24 || _currentHeaderImage == null)
                {
                    _lastCheckTime = now;
                    _currentHeaderImage = GetNewHeaderImage();
                }

                return _currentHeaderImage;
            }
        }

        public static void Load()
        {
            _headerImageList = _repository.GetList();
        }

        private static HeaderImageData GetNewHeaderImage()
        {
            if (_currentHeaderImage == null)
            {
                if (_headerImageList.Count == 0)
                {
                    throw new IndexOutOfRangeException("There is no loaded header images!");
                }

                return _headerImageList[0];
            }

            var index = _headerImageList.IndexOf(_currentHeaderImage);

            //If there is last image
            if (index == _headerImageList.Count - 1)
            {
                return _headerImageList[0];
            }

            return _headerImageList[index + 1];
        }
    }
}

using System;
using System.Collections.Generic;
using Models.Data;

namespace ZaBugrom.Managers
{
    public static class HeaderImageManager
    {
        private static object _locker = new object();

        private static List<HeaderImageData> _headerImageList;
        private static HeaderImageData _currentHeaderImage;
        private static DateTime _lastCheckTime;

        public static HeaderImageData CurrentHeaderImage
        {
            get
            {
                 lock (_locker)
                {
                    var now = DateTime.Now;

                    if ((now - _lastCheckTime).TotalHours > 24 || _currentHeaderImage == null)
                    {
                        _lastCheckTime = now;
                        _currentHeaderImage = GetNewHeaderImage();
                    }
                }

                return _currentHeaderImage;
            }
        }

        public static void Load()
        {
            _headerImageList = RepositoryManager.HeaderImageRepository.GetList();
        }

        private static HeaderImageData GetNewHeaderImage()
        {
            if (_currentHeaderImage == null)
            {
                if (_headerImageList.Count == 0)
                {
                    return null;
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

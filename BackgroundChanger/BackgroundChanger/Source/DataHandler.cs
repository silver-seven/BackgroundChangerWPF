using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BackgroundChanger.Source
{
    class DataHandler
    {
        private IList<string> pathList = new List<string>();
        private IList<string> favList = new List<string>();
        private IList<string> excludeList = new List<string>();
        private string fileName;
        private XMLHandler xml = new XMLHandler();
        private string[] xmlRootNodes;
        private string[] xmlRootNodes2; /*Timer, BufferSize, WallpaperStyle, FavFreq*/
        private int[] appParams; /*Timer, BufferSize, WallpaperStyle, FavFreq*/

        public DataHandler()
        {
            fileName = "user.xml";
            xmlRootNodes = new string[] { "Paths", "Favorites", "Excludes" };
            xmlRootNodes2 = new string[] { "Timer", "BufferSize", "WallpaperStyle", "favFreq" };
           
            //Load/Create xml file.
            if (File.Exists(fileName))
            {
                xml.Load();
                for (int i = 0; i < xmlRootNodes2.Count(); i++)
                {
                    appParams[i] = int.Parse(xml.GetXMLNode("//" + xmlRootNodes2[i]));
                }
                xml.GetXMLNodes("//Paths", ref pathList);
                ///////////////////////////////TODO: NEED TO LOAD TO LIST; FAV AND EXCLUDE
            }
            else
            {
                appParams = new int[] { 600, 100, 0, 10 };
                xml.Create(fileName);
                foreach(string str in xmlRootNodes)
                {
                    xml.AddToRoot(str);
                }
                for(int i=0; i < xmlRootNodes2.Count(); i++)
                {
                    xml.AddToRoot(xmlRootNodes2[i], appParams[i].ToString());
                }
            }
        }

        public void SaveData()
        {
            xml.Save();
        }

        //add
        public void AddPath(string path)
        {
            pathList.Add(path);
        }
        public void AddFav(string fav)
        {
            favList.Add(fav);
        }
        public void AddExclude(string exc)
        {
            excludeList.Add(exc);
        }
        //remove
        public void RemovePath(string path)
        {
            pathList.Remove(path);
        }
        public void RemoveFav(string fav)
        {
            favList.Remove(fav);
        }
        public void RemoveExclude(string exc)
        {
            excludeList.Remove(exc);
        }

        //get element
        public string[] GetAllPaths()
        {   
            return pathList.ToArray();
        }
        public string[] GetAllFavs()
        {
            return favList.ToArray();
        }
        public string[] GetAllExcludes()
        {
            return excludeList.ToArray();
        }
        public int GetTimer()
        {
            return appParams[0];
        }
        public int GetBufferSize()
        {
            return appParams[1];
        }
        public int GetWallpaperStyle()
        {
            return appParams[2];
        }
        public int GetFavFreq()
        {
            return appParams[3];
        }

        //set
        public void SetTimer(int data)
        {
            appParams[0] = data;
        }
        public void SetBufferSize(int data)
        {
            appParams[1] = data;
        }
        public void SetWallpaperStyle(int data)
        {
            appParams[2] = data;
        }
        public void SetFavFreq(int data)
        {
            appParams[3] = data;
        }
    }
}

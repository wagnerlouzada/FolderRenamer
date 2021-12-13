using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderRenamer
{
    #region xxx

    //public List<DbMovieInfo> GetTMDBinfos(string otitle, string ttitle, int year, string director, string fanartPath, bool multiImage, bool choose, string masterTitle, string language)
    //{
    //    var listemovies = new List<DbMovieInfo>();
    //    if (otitle.Length == 0) return listemovies;
    //    string wtitle1 = otitle;
    //    string wtitle2 = ttitle;
    //    if (otitle.IndexOf("\\") > 0)
    //        wtitle1 = wtitle1.Substring(wtitle1.IndexOf("\\") + 1);
    //    if (ttitle.IndexOf("\\") > 0)
    //        wtitle2 = wtitle2.Substring(wtitle2.IndexOf("\\") + 1);
    //    if (ttitle.Length == 0)
    //        ttitle = otitle;
    //    var theMoviedb = new TheMoviedb();
    //    listemovies = theMoviedb.GetMoviesByTitles(wtitle1, wtitle2, year, director, "", null, choose, language);

    //    string filename = string.Empty;
    //    string filename1 = string.Empty;
    //    string filename2 = string.Empty;
    //    if (masterTitle == "OriginalTitle")
    //        wtitle2 = wtitle1;
    //    if (listemovies.Count == 1 && listemovies[0].Posters != null && listemovies[0].Posters.Count > 0 && !choose)
    //    {
    //        bool first = true;
    //        foreach (string backdrop in listemovies[0].Posters)
    //        {
    //            filename1 = GrabUtil.DownloadCovers(fanartPath, backdrop, wtitle2, multiImage, first, out filename);
    //            //if (filename2 == string.Empty)
    //            //    filename2 = filename1;
    //            if ((filename2 != "added") && (filename1 != "already"))
    //                filename2 = "added";
    //            else
    //                filename2 = "already";
    //            first = false;
    //        }
    //        listemovies[0].Name = filename2;
    //    }
    //    else if (listemovies.Count > 1)

    //        //listemovies[0].Name = "(toomany)";
    //        listemovies[0].Name = "(toomany) - (" + listemovies.Count + " results) - " + listemovies[0].Name;

    //    return listemovies;
    //}

    ////-------------------------------------------------------------------------------------------
    ////  Dowload backdrops on theMovieDB.org
    ////-------------------------------------------------------------------------------------------        
    //public static void Download_Backdrops_Fanart(string wtitle, string wttitle, string wftitle, string director, string imdbid, string year, bool choose, int wGetId, string savetitle, string personartworkpath, bool loadFanart, bool loadPersonImages, GUIAnimation searchanimation)
    //{
    //    new Thread(delegate ()
    //    {
    //        var grab = new GrabberUrlClass();
    //        int wyear = 0;
    //        try { wyear = Convert.ToInt32(year); } catch { }
    //        try
    //        {
    //            SetProcessAnimationStatus(true, searchanimation);  // GUIWaitCursor.Init(); GUIWaitCursor.Show();
    //            List<DbMovieInfo> listemovies = grab.GetFanart(wtitle, savetitle, wyear, director, imdbid, MyFilms.conf.StrPathFanart, true, choose, MyFilms.conf.StrTitle1, personartworkpath);
    //            SetProcessAnimationStatus(false, searchanimation);  //GUIWaitCursor.Hide();
    //                                                                //System.Collections.Generic.List<grabber.DBMovieInfo> listemovies = Grab.GetFanart(wtitle, wttitle, wyear, director, MyFilms.conf.StrPathFanart, true, choose);
    //            LogMyFilms.Debug("(DownloadBackdrops) - listemovies: '" + wtitle + "', '" + wttitle + "', '" + wyear + "', '" + director + "', '" + MyFilms.conf.StrPathFanart + "', 'true', '" + choose + "', '" + MyFilms.conf.StrTitle1 + "'");
    //            int listCount = listemovies.Count;
    //            LogMyFilms.Debug("(DownloadBackdrops) - listemovies: Result Listcount: '" + listCount + "'");

    //            if (choose) listCount = 2;
    //            switch (listCount)
    //            {
    //                case 0:
    //                    break;
    //                case 1:
    //                    LogMyFilms.Debug("Fanart " + listemovies[0].Name.Substring(listemovies[0].Name.LastIndexOf("\\") + 1) + " downloaded for " + wttitle);
    //                    if (listemovies[0].Persons.Count > 0)
    //                    {
    //                        LogMyFilms.Debug("PersonArtwork: " + listemovies[0].Persons.Count + " Persons checked for " + wttitle);
    //                        foreach (DbPersonInfo person in listemovies[0].Persons)
    //                        {
    //                            LogMyFilms.Debug("PersonArtwork: " + person.Images.Count + " images found for " + person.Name);
    //                        }
    //                    }
    //                    break;
    //                default:

    //                    const int MinChars = 2;
    //                    const bool Filter = true; // no "der die das"

    //                    GUIDialogMenu dlg = (GUIDialogMenu)GUIWindowManager.GetWindow((int)GUIWindow.Window.WINDOW_DIALOG_MENU);
    //                    if (dlg == null) return;
    //                    dlg.Reset();
    //                    dlg.SetHeading(loadFanart ? GUILocalizeStrings.Get(1079862) : GUILocalizeStrings.Get(1079900));  // Load fanart (online)  // Download person images (selected film)
    //                    dlg.Add("  *****  " + GUILocalizeStrings.Get(1079860) + "  *****  "); //manual selection
    //                    foreach (DbMovieInfo t in listemovies)
    //                    {
    //                        string dialoginfoline = t.Name + "  (" + t.Year + ")";
    //                        if (loadFanart) dialoginfoline += " - Fanarts: " + t.Backdrops.Count;
    //                        if (loadPersonImages) dialoginfoline += " - Persons: " + t.Persons.Count.ToString();
    //                        dlg.Add(dialoginfoline);
    //                        LogMyFilms.Debug("TMDB listemovies: " + t.Name + "  (" + t.Year + ") - Fanarts: " + t.Backdrops.Count + " - TMDB-Id: " + t.Identifier + " - Persons: " + t.Persons.Count);
    //                    }
    //                    if (!(dlg.SelectedLabel > -1))
    //                    {
    //                        dlg.SelectedLabel = -1;
    //                        dlg.DoModal(wGetId);
    //                    }
    //                    if (dlg.SelectedLabel == 0)
    //                    {
    //                        #region Get SubTitles and Subwords from otitle and ttitle
    //                        //First Show Dialog to choose Otitle, Ttitle or substrings - or Keyboard to manually enter searchstring!!!
    //                        var dlgSearchFilm = (GUIDialogMenu)GUIWindowManager.GetWindow((int)GUIWindow.Window.WINDOW_DIALOG_MENU);
    //                        if (dlgSearchFilm == null) return;
    //                        dlgSearchFilm.Reset();
    //                        dlgSearchFilm.SetHeading(GUILocalizeStrings.Get(1079859)); // choose search expression
    //                        dlgSearchFilm.Add("  *****  " + GUILocalizeStrings.Get(1079858) + "  *****  ");
    //                        //manual selection with keyboard
    //                        //dlgs.Add(wtitle); //Otitle
    //                        dlgSearchFilm.Add(savetitle); //Otitle = savetitle
    //                        dlgSearchFilm.Add(wttitle); //Ttitle
    //                        foreach (string t in MyFilms.SubTitleGrabbing(wtitle).Where(t => t.Length > 1)) dlgSearchFilm.Add(t);
    //                        foreach (string t in MyFilms.SubTitleGrabbing(wttitle).Where(t => t.Length > 1)) dlgSearchFilm.Add(t);
    //                        foreach (string t in MyFilms.SubWordGrabbing(wtitle, MinChars, Filter).Where(t => t.Length > 1)) dlgSearchFilm.Add(t);
    //                        foreach (string t in MyFilms.SubWordGrabbing(wttitle, MinChars, Filter).Where(t => t.Length > 1)) dlgSearchFilm.Add(t);
    //                        //Now all titles and Substrings listed in dialog !
    //                        //dlgs.Add("  *****  " + GUILocalizeStrings.Get(1079860) + "  *****  "); //manual selection
    //                        if (!(dlgSearchFilm.SelectedLabel > -1))
    //                        {
    //                            dlgSearchFilm.SelectedLabel = -1;
    //                            dlgSearchFilm.DoModal(wGetId);
    //                        }
    //                        if (dlgSearchFilm.SelectedLabel == 0) // enter manual searchstring via VK
    //                        {
    //                            VirtualKeyboard keyboard = (VirtualKeyboard)GUIWindowManager.GetWindow((int)GUIWindow.Window.WINDOW_VIRTUAL_KEYBOARD);
    //                            if (null == keyboard) return;
    //                            keyboard.Reset();
    //                            keyboard.SetLabelAsInitialText(false); // set to false, otherwise our intial text is cleared
    //                            keyboard.Text = wtitle;
    //                            keyboard.DoModal(wGetId);
    //                            if (keyboard.IsConfirmed && (keyboard.Text.Length > 0))
    //                            {
    //                                //Remove_Backdrops_Fanart(wtitle, true);
    //                                //Remove_Backdrops_Fanart(wttitle, true);
    //                                //Remove_Backdrops_Fanart(wftitle, true);
    //                                Download_Backdrops_Fanart(keyboard.Text, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, true, wGetId, savetitle, personartworkpath, loadFanart, loadPersonImages, searchanimation);
    //                            }
    //                            break;
    //                        }
    //                        if (dlgSearchFilm.SelectedLabel > 0 && dlgSearchFilm.SelectedLabel < 3) // if one of otitle or ttitle selected, keep year and director
    //                        {
    //                            Download_Backdrops_Fanart(dlgSearchFilm.SelectedLabelText, wttitle, wftitle, year, director, string.Empty, true, wGetId, savetitle, personartworkpath, loadFanart, loadPersonImages, searchanimation);
    //                            //Download_Backdrops_Fanart(string wtitle, string wttitle, string director, string year, bool choose,int wGetID, string savetitle)
    //                            break;
    //                        }
    //                        if (dlgSearchFilm.SelectedLabel > 2) // For subitems, search without year and director !
    //                        {
    //                            Download_Backdrops_Fanart(dlgSearchFilm.SelectedLabelText, wttitle, wftitle, string.Empty, string.Empty, string.Empty, true, wGetId, savetitle, personartworkpath, loadFanart, loadPersonImages, searchanimation);
    //                            //Download_Backdrops_Fanart(string wtitle, string wttitle, string director, string year, bool choose,int wGetID, string savetitle)
    //                            break;
    //                        }

    //                        #endregion
    //                    }
    //                    if (dlg.SelectedLabel > 0)
    //                    {
    //                        // Load Fanart  -> show progress dialog !

    //                        var dlgPrgrs = (GUIDialogProgress)GUIWindowManager.GetWindow((int)GUIWindow.Window.WINDOW_DIALOG_PROGRESS);
    //                        if (dlgPrgrs != null)
    //                        {
    //                            dlgPrgrs.Reset();
    //                            dlgPrgrs.DisplayProgressBar = true;
    //                            dlgPrgrs.ShowWaitCursor = false;
    //                            dlgPrgrs.DisableCancel(true);
    //                            dlgPrgrs.SetHeading("MyFilms Artwork Download");
    //                            dlgPrgrs.StartModal(GUIWindowManager.ActiveWindow);
    //                            dlgPrgrs.SetLine(1, "Loading Artwork ...");
    //                            dlgPrgrs.Percentage = 0;

    //                            #region load fanarts ...
    //                            bool first = true;
    //                            string filename = string.Empty;
    //                            string filename1 = string.Empty;
    //                            //if (MyFilms.conf.StrTitle1 == "OriginalTitle")
    //                            //  wttitle = savetitle; // Was wttitle = wtitle;
    //                            int i = 0;
    //                            if (loadFanart) // Download Fanart
    //                            {
    //                                if (dlgPrgrs != null) dlgPrgrs.SetLine(1, "Loading Fanart for '" + savetitle + "'");

    //                                foreach (string backdrop in listemovies[dlg.SelectedLabel - 1].Backdrops)
    //                                {
    //                                    filename1 = Grabber.GrabUtil.DownloadBacdropArt(MyFilms.conf.StrPathFanart, backdrop, savetitle, true, first, out filename);
    //                                    if (dlgPrgrs != null) dlgPrgrs.SetLine(2, "loading '" + System.IO.Path.GetFileName(filename) + "'");
    //                                    if (dlgPrgrs != null) dlgPrgrs.Percentage = i * 100 / listemovies[dlg.SelectedLabel - 1].Backdrops.Count;
    //                                    LogMyFilms.Debug("Fanart " + filename1.Substring(filename1.LastIndexOf("\\") + 1) + " downloaded for " + savetitle);

    //                                    if (filename == string.Empty) filename = filename1;
    //                                    if (!(filename == "already" && filename1 == "already")) filename = "added";
    //                                    first = false;
    //                                    i++;
    //                                }
    //                            }
    //                            #endregion

    //                            listemovies[0].Name = filename;

    //                            if (loadPersonImages) // Download PersonArtwork
    //                            {
    //                                string filenameperson = string.Empty;
    //                                string filename1person = string.Empty;
    //                                LogMyFilms.Debug("Person Artwork - " + listemovies[0].Persons.Count + " persons found - now loading artwork");
    //                                if (!string.IsNullOrEmpty(personartworkpath) && listemovies[0].Persons != null &&
    //                                    listemovies[0].Persons.Count > 0)
    //                                {
    //                                    if (dlgPrgrs != null) dlgPrgrs.SetLine(1, "Loading person images for '" + wttitle + "'");
    //                                    if (dlgPrgrs != null) dlgPrgrs.SetLine(2, "");

    //                                    foreach (DbPersonInfo person in listemovies[0].Persons)
    //                                    {
    //                                        bool firstpersonimage = true;
    //                                        bool onlysinglepersonimage = true;
    //                                        var persondetails = new DbPersonInfo();
    //                                        var theMoviedb = new TheMoviedb();
    //                                        persondetails = theMoviedb.GetPersonsById(person.Id, string.Empty);
    //                                        LogMyFilms.Debug("Person Artwork: found '" + persondetails.Images.Count + "' TMDB images for '" + persondetails.Name + "' in movie '" + savetitle + "'");
    //                                        if (dlgPrgrs != null) dlgPrgrs.SetLine(2, "loading '" + persondetails.Name + "'");
    //                                        if (dlgPrgrs != null) dlgPrgrs.Percentage = 0;

    //                                        if (persondetails.Images.Count > 0)
    //                                        {
    //                                            i = 0;
    //                                            foreach (var image in persondetails.Images)
    //                                            {
    //                                                filename1person = Grabber.GrabUtil.DownloadPersonArtwork(personartworkpath, image, persondetails.Name, true, firstpersonimage, out filenameperson);
    //                                                if (dlgPrgrs != null) dlgPrgrs.SetLine(2, "loading '" + persondetails.Name + "' (TMDB - #" + i + ")");
    //                                                if (dlgPrgrs != null) dlgPrgrs.Percentage = i * 100 / persondetails.Images.Count;

    //                                                LogMyFilms.Debug("Person Artwork " + filename1person.Substring(filename1person.LastIndexOf("\\") + 1) + " downloaded for '" + persondetails.Name + "' in movie '" + savetitle + "', path='" + filename1person + "'");
    //                                                if (filenameperson == string.Empty) filenameperson = filename1person;
    //                                                if (!(filenameperson == "already" && filename1person == "already")) filenameperson = "added";
    //                                                firstpersonimage = false;
    //                                                i++;
    //                                                if (onlysinglepersonimage) break;
    //                                            }
    //                                        }
    //                                    }
    //                                }
    //                                else if (string.IsNullOrEmpty(personartworkpath)) LogMyFilms.Debug("No Personartwork loaded - Personartworkpath is not set in setup!");
    //                            }
    //                            if (dlgPrgrs != null) dlgPrgrs.Percentage = 100;
    //                            dlgPrgrs.ShowWaitCursor = false;
    //                            dlgPrgrs.SetLine(1, GUILocalizeStrings.Get(1079846));
    //                            dlgPrgrs.SetLine(2, "");
    //                            Thread.Sleep(50);
    //                            dlgPrgrs.Close(); // Done...
    //                            return;
    //                        }
    //                    }
    //                    break;
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            LogMyFilms.Debug(ex, "Thread 'MyFilmsTMDBLoader' - exception!");
    //        }
    //        GUIWindowManager.SendThreadCallbackAndWait((p1, p2, data) =>
    //        {
    //            //dlgPrgrs.ShowWaitCursor = false;
    //            // enter here what to load after background thread has finished !
    //            if (DetailsUpdated != null) DetailsUpdated(true);
    //            return 0;
    //        }, 0, 0, null);
    //    })
    //    { Name = "MyFilmsTMDBLoader", IsBackground = true }.Start();
    //}

    //public List<DbMovieInfo> GetFanart(string otitle, string ttitle, int year, string director, string imdbid, string fanartPath, bool multiImage, bool choose, string masterTitle, string personArtworkPath = "", int downloadlimit = 0, string resolutionMin = "", string resolutionMax = "")
    //{
    //    string language = CultureInfo.CurrentCulture.Name.Substring(0, 2); // use local language instead

    //    List<DbMovieInfo> listemovies = new List<DbMovieInfo>();
    //    if (otitle.Length == 0) return listemovies;
    //    if (ttitle.Length == 0) ttitle = otitle;
    //    string wtitle1 = otitle;
    //    string wtitle2 = ttitle;
    //    if (otitle.IndexOf("\\") > 0) wtitle1 = wtitle1.Substring(wtitle1.IndexOf("\\") + 1);
    //    if (ttitle.IndexOf("\\") > 0) wtitle2 = wtitle2.Substring(wtitle2.IndexOf("\\") + 1);
    //    var theMoviedb = new TheMoviedb();
    //    listemovies = theMoviedb.GetMoviesByTitles(wtitle1, wtitle2, year, director, imdbid, null, choose, language);

    //    string filename = string.Empty;
    //    string filename1 = string.Empty;
    //    string filename2 = string.Empty;
    //    if (masterTitle == "OriginalTitle")
    //        wtitle2 = wtitle1;
    //    if (listemovies.Count == 1 && listemovies[0].Backdrops != null && listemovies[0].Backdrops.Count > 0 && !choose)
    //    {
    //        // Download Fanart !!!
    //        bool first = true;
    //        foreach (string backdrop in listemovies[0].Backdrops)
    //        {
    //            // old: filename1 = GrabUtil.DownloadBacdropArt(fanartPath, backdrop, wtitle2, multiImage, first, out filename);
    //            filename1 = GrabUtil.DownloadBacdropArt(fanartPath, backdrop, wtitle2, multiImage, first, out filename, downloadlimit, resolutionMin, resolutionMax);
    //            //if (filename2 == string.Empty)
    //            //    filename2 = filename1;
    //            if ((filename2 != "added") && (filename1 != "already") && !filename1.StartsWith("numberlimit") && !filename1.StartsWith("resolution"))
    //            {
    //                filename2 = "added";
    //            }
    //            else
    //            {
    //                if (filename1.StartsWith("numberlimit"))
    //                    filename2 = "numberlimit";
    //                else if (filename1.StartsWith("resolution"))
    //                {
    //                    filename2 = "resolution";
    //                }
    //                else
    //                {
    //                    filename2 = "already";
    //                    first = false;
    //                }
    //            }

    //        }
    //        listemovies[0].Name = filename2;

    //        #region Download PersonArtwork (disabled)
    //        //// Get Actors from TMDB
    //        //string filenameperson = string.Empty;
    //        //string filename1person = string.Empty;
    //        //string filename2person = string.Empty;
    //        ////string ImdbBaseUrl = "http://www.imdb.com/";
    //        //if (!string.IsNullOrEmpty(personArtworkPath) && listemovies[0].Persons != null && listemovies[0].Persons.Count > 0)
    //        //{
    //        //  List<grabber.DBPersonInfo> listepersons = listemovies[0].Persons;
    //        //  foreach (grabber.DBPersonInfo person in listepersons)
    //        //  {
    //        //    bool firstpersonimage = true;
    //        //    grabber.DBPersonInfo persondetails = new DBPersonInfo();
    //        //    persondetails = TheMoviedb.getPersonsById(person.Id, string.Empty);
    //        //    foreach (var image in persondetails.Images)
    //        //    {
    //        //      filename1person = GrabUtil.DownloadPersonArtwork(personArtworkPath, image, persondetails.Name, multiImage, firstpersonimage, out filenameperson);
    //        //      if ((filename2person != "added") && (filename1person != "already"))
    //        //        filename2person = "added";
    //        //      else
    //        //        filename2person = "already";
    //        //      firstpersonimage = false;
    //        //    }
    //        //    //// Get further IMDB images
    //        //    //Grabber.MyFilmsIMDB _imdb = new Grabber.MyFilmsIMDB();
    //        //    //Grabber.MyFilmsIMDB.IMDBUrl wurl;
    //        //    //_imdb.FindActor(persondetails.Name);
    //        //    //IMDBActor imdbActor = new IMDBActor();

    //        //    //if (_imdb.Count > 0)
    //        //    //{
    //        //    //  string url = string.Empty;
    //        //    //  wurl = (Grabber.MyFilmsIMDB.IMDBUrl)_imdb[0]; // Assume first match is the best !
    //        //    //  if (wurl.URL.Length != 0)
    //        //    //  {
    //        //    //    url = wurl.URL;
    //        //    //    //url = wurl.URL + "videogallery"; // Assign proper Webpage for Actorinfos
    //        //    //    //url = ImdbBaseUrl + url.Substring(url.IndexOf("name"));
    //        //    //    this.GetActorDetails(url, persondetails.Name, false, out imdbActor);
    //        //    //    filename1person = GrabUtil.DownloadPersonArtwork(personArtworkPath, imdbActor.ThumbnailUrl, persondetails.Name, multiImage, firstpersonimage, out filenameperson);
    //        //    //    firstpersonimage = false;
    //        //    //  }
    //        //    //}
    //        //  }
    //        //  //// Get further Actors from IMDB
    //        //  //IMDBMovie MPmovie = new IMDBMovie();
    //        //  //MPmovie.Title = listemovies[0].Name;
    //        //  //MPmovie.IMDBNumber = listemovies[0].ImdbID;
    //        //  //FetchActorsInMovie(MPmovie, personArtworkPath);
    //        //}
    //        #endregion
    //    }
    //    else if (listemovies.Count > 1)
    //    {
    //        //listemovies[0].Name = "(toomany)";
    //        listemovies[0].Name = "(toomany) - (" + listemovies.Count + " results) - " + listemovies[0].Name;
    //    }
    //    return listemovies;
    //}

    //static void downloadingWorker_DoWork(object sender, DoWorkEventArgs e)
    //{
    //    var tmdbapi = new TheMoviedb();
    //    do
    //    {
    //        if (downloadingWorker.CancellationPending)
    //        {
    //            LogMyFilms.Debug("cancel person info updater...");
    //            return;
    //        }

    //        DbPersonInfo f;
    //        setDownloadStatus();
    //        lock (PersonstoDownloadQueue)
    //        {
    //            f = PersonstoDownloadQueue.Dequeue();
    //        }
    //        bool bDownloadSuccess = true;

    //        try
    //        {
    //            #region download person image

    //            bDownloadSuccess = UpdatePersonDetails(f.Name, null, false, false);

    //            #region experimental TMDB v3 code...
    //            //Grabber.TMDBv3.Tmdb api = new Grabber.TMDBv3.Tmdb("apikey", "de"); // language is optional, default is "en"
    //            //TmdbConfiguration tmdbConf = api.GetConfiguration();
    //            //TmdbPersonSearch person = api.SearchPerson("name", 1);
    //            //List<PersonResult> persons = person.results;
    //            //PersonResult pinfo = persons[0];
    //            //TmdbPerson singleperson = api.GetPersonInfo(pinfo.id);
    //            //TmdbPersonCredits personFilmList = api.GetPersonCredits(pinfo.id);

    //            // Search
    //            //TmdbMovieSearch SearchMovie(string query, int page)
    //            //TmdbPersonSearch SearchPerson(string query, int page)
    //            //TmdbCompanySearch SearchCompany(string query, int page);             

    //            // Person Info
    //            //TmdbPerson GetPersonInfo(int PersonID)
    //            //TmdbPersonCredits GetPersonCredits(int PersonID)
    //            //TmdbPersonImages GetPersonImages(int PersonID)
    //            //Movie Info
    //            //TmdbMovie GetMovieInfo(int MovieID)
    //            //TmdbMovie GetMovieByIMDB(string IMDB_ID)
    //            //TmdbMovieAlternateTitles GetMovieAlternateTitles(int MovieID, string Country)
    //            //TmdbMovieCast GetMovieCast(int MovieID)
    //            //TmdbMovieImages GetMovieImages(int MovieID)
    //            //TmdbMovieKeywords GetMovieKeywords(int MovieID)
    //            //TmdbMovieReleases GetMovieReleases(int MovieID)
    //            //TmdbMovieTrailers GetMovieTrailers(int MovieID)
    //            //TmdbSimilarMovies GetSimilarMovies(int MovieID, int page)
    //            //TmdbTranslations GetMovieTranslations(int MovieID)

    //            // Social Movie Info
    //            //TmdbNowPlaying GetNowPlayingMovies(int page)
    //            //TmdbPopular GetPopularMovies(int page)
    //            //TmdbTopRated GetTopRatedMovies(int page)
    //            //TmdbUpcoming GetUpcomingMovies(int page)
    //            #endregion

    //            #region TMDBv3 loading (inactive)
    //            //string language = CultureInfo.CurrentCulture.Name.Substring(0, 2);
    //            //List<DbPersonInfo> personlist = tmdbapi.GetPersonsByName(f.Name, false, language);
    //            //if (personlist.Count == 0)
    //            //{
    //            //  LogMyFilms.Debug("downloadingWorker_DoWork() - Person '" + f.Name + "' not found on TMDB, remaining items: '" + PersonstoDownloadQueue.Count + "'");
    //            //  bDownloadSuccess = false;
    //            //}
    //            //else
    //            //{
    //            //  f = personlist[0];

    //            //  if (f != null && !File.Exists(Path.Combine(MyFilms.conf.StrPathArtist, f.Name)))
    //            //  {
    //            //    if (f.Images.Count == 0)
    //            //    {
    //            //      LogMyFilms.Debug("downloadingWorker_DoWork() - Person '" + f.Name + "' found, but no images available on TMDB! - remaining items: '" + PersonstoDownloadQueue.Count + "'");
    //            //      bDownloadSuccess = false;
    //            //    }
    //            //    else
    //            //    {
    //            //      string filename = Path.Combine(MyFilms.conf.StrPathArtist, f.Name);

    //            //      LogMyFilms.Debug("downloadingWorker_DoWork() - TMDB Image found for person '" + f.Name + "', URL = '" + f.Images[0] + "' - remaining items: '" + PersonstoDownloadQueue.Count + "'");
    //            //      string filename1person = Grabber.GrabUtil.DownloadPersonArtwork(MyFilms.conf.StrPathArtist, f.Images[0], f.Name, false, true, out filename);
    //            //      LogMyFilms.Debug("Person Image (TMDB) '" + filename1person.Substring(filename1person.LastIndexOf("\\") + 1) + "' downloaded for '" + f.Name + "', path = '" + filename1person + "', filename = '" + filename + "'");

    //            //      if (downloadingWorker.CancellationPending)
    //            //      {
    //            //        bDownloadSuccess = false;
    //            //        LogMyFilms.Debug("cancel person image download - last person: " + f.Name);
    //            //        lock (PersonstoDownloadQueue)
    //            //        {
    //            //          PersonstoDownloadQueue.Clear();
    //            //        }
    //            //        return;
    //            //      }
    //            //      Application.DoEvents();
    //            //    }
    //            //  }
    //            //}
    //            #endregion

    //            #region try IMDB, if TMDB was not successful ! (inactive)
    //            //if (!bDownloadSuccess)
    //            //{
    //            //  // LogMyFilms.Debug("downloadingWorker_DoWork() - TMDB unsuccessful - try IMDB ...");
    //            //  bDownloadSuccess = true;
    //            //  var imdb = new IMDB();
    //            //  imdb.FindActor(f.Name);

    //            //  if (imdb.Count == 0)
    //            //  {
    //            //    LogMyFilms.Debug("downloadingWorker_DoWork() - Person '" + f.Name + "' not found on IMDB, remaining items: '" + PersonstoDownloadQueue.Count + "'");
    //            //    bDownloadSuccess = false;
    //            //  }
    //            //  else
    //            //  {
    //            //    if (imdb[0].URL.Length != 0)
    //            //    {
    //            //      var imdbActor = new IMDBActor();
    //            //      //#if MP1X
    //            //      //                    _imdb.GetActorDetails(_imdb[0], out imdbActor);
    //            //      //#else
    //            //      //                    _imdb.GetActorDetails(_imdb[0], false, out imdbActor);
    //            //      //#endif
    //            //      GUIUtils.GetActorDetails(imdb, imdb[0], false, out imdbActor);
    //            //      if (imdbActor.ThumbnailUrl.Length > 0)
    //            //      {
    //            //        LogMyFilms.Debug("downloadingWorker_DoWork() - IMDB Image found for person '" + f.Name + "', URL = '" + imdbActor.ThumbnailUrl + "' - remaining items: '" + PersonstoDownloadQueue.Count + "'");
    //            //        string filename = Path.Combine(MyFilms.conf.StrPathArtist, f.Name);
    //            //        string filename1person = GrabUtil.DownloadPersonArtwork(MyFilms.conf.StrPathArtist, imdbActor.ThumbnailUrl, f.Name, false, true, out filename);
    //            //        LogMyFilms.Debug("Person Image (IMDB) '" + filename1person.Substring(filename1person.LastIndexOf("\\") + 1) + "' downloaded for '" + f.Name + "', path = '" + filename1person + "', filename = '" + filename + "'");
    //            //      }
    //            //      else
    //            //      {
    //            //        LogMyFilms.Debug("downloadingWorker_DoWork() - Person '" + f.Name + "' found, but no images available on IMDB! - remaining items: '" + PersonstoDownloadQueue.Count + "'");
    //            //        bDownloadSuccess = false;
    //            //      }
    //            //    }
    //            //    else
    //            //    {
    //            //      LogMyFilms.Debug("downloadingWorker_DoWork() - Person '" + f.Name + "' found, but no images available on IMDB! - remaining items: '" + PersonstoDownloadQueue.Count + "'");
    //            //      bDownloadSuccess = false;
    //            //    }
    //            //  }
    //            //}
    //            #endregion

    //            if (downloadingWorker.CancellationPending)
    //            {
    //                LogMyFilms.Debug("cancel person info download - last person: " + f.Name);
    //                lock (PersonstoDownloadQueue)
    //                {
    //                    PersonstoDownloadQueue.Clear();
    //                }
    //                return;
    //            }

    //            // LogMyFilms.Debug("Result of person info download for '" + f.Name + "': success = '" + bDownloadSuccess + "'");
    //            if (bDownloadSuccess) downloadingWorker.ReportProgress(0, f.Name);
    //            #endregion
    //        }
    //        catch (Exception ex) { LogMyFilms.Debug(ex, "Error loading person updates: '" + ex.Message + "'"); }
    //    }
    //    while (PersonstoDownloadQueue.Count > 0 && !downloadingWorker.CancellationPending);
    //}

    #endregion

    class Trash
    {
    }

}

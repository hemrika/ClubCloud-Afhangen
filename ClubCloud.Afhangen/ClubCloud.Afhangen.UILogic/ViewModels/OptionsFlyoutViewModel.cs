namespace ClubCloud.Afhangen.UILogic.ViewModels
{
    using Microsoft.Practices.Prism.StoreApps;
    using Microsoft.Practices.Prism.StoreApps.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Windows.ApplicationModel;
    using Windows.Globalization;
    using ClubCloud.Afhangen.UILogic.Models;
    using System.Linq;
    using Microsoft.Practices.Prism.Mvvm;
    using Microsoft.Practices.Prism.Commands;
    using Microsoft.Practices.Prism.Mvvm.Interfaces;
    using Windows.Storage;

    public class OptionsFlyoutViewModel : ViewModel, IFlyoutViewModel, IView
    {
        private Action closeFlyout;
        private string initialLanguage;
        private bool isRestartRequired;
        private string _nummer;

        public OptionsFlyoutViewModel()
        {
            initialLanguage = ApplicationLanguages.PrimaryLanguageOverride;

            ChangeVerenigingCommand = new DelegateCommand<string>(ChangeVereniging);

            InitializeLanguages();
        }

        public ICommand ChangeVerenigingCommand { get; private set; }

        public string Nummer
        {
            get
            {
                object VerenigingsId;
                if (ApplicationData.Current.LocalSettings.Values.TryGetValue("VerenigingsId", out VerenigingsId))
                    _nummer = VerenigingsId.ToString();

                return _nummer;
            }
            set
            {
                int number;
                if (int.TryParse(value, out number))
                    ApplicationData.Current.LocalSettings.Values["VerenigingsId"] = number;

            }
        }

        public bool IsRestartRequired
        {
            get { return isRestartRequired; }
            set { SetProperty(ref isRestartRequired, value); }
        }

        private void InitializeLanguages()
        {
            object VerenigingsId;
            if(ApplicationData.Current.LocalSettings.Values.TryGetValue("VerenigingsId", out VerenigingsId))
                _nummer = VerenigingsId.ToString();

            /*
            var languages = new List<ApplicationLanguage>();

            foreach (var langTag in ApplicationLanguages.ManifestLanguages)
            {
                var lang = new Windows.Globalization.Language(langTag);
                var name = (lang.NativeName == lang.DisplayName) ? lang.DisplayName : lang.DisplayName + " - " + lang.NativeName;

                languages.Add(new ApplicationLanguage { Tag = langTag, DisplayName = name });
            }

            ManifestLanguages = new ObservableCollection<ApplicationLanguage>(languages);
            SelectedLanguage = ManifestLanguages.FirstOrDefault(x => x.Tag == ApplicationLanguages.PrimaryLanguageOverride);
            */
        }

        private void ChangeVereniging(string verenigingsid)
        {
            int number;
            if(int.TryParse(verenigingsid, out number))
                ApplicationData.Current.LocalSettings.Values["VerenigingsId"] = verenigingsid;
            //IsRestartRequired = initialLanguage != selectedLanguage.Tag;

            //ApplicationLanguages.PrimaryLanguageOverride = selectedLanguage.Tag;
        }

        public Action CloseFlyout
        {
            get { return closeFlyout; }
            set { SetProperty(ref closeFlyout, value); }
        }

        public object DataContext
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}

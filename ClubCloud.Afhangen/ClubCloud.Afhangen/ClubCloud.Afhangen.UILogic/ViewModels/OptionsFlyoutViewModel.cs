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

    public class OptionsFlyoutViewModel : ViewModel, IFlyoutViewModel
    {
        private Action closeFlyout;
        private string initialLanguage;
        private bool isRestartRequired;

        public OptionsFlyoutViewModel()
        {
            initialLanguage = ApplicationLanguages.PrimaryLanguageOverride;

            ChangeLanguageCommand = new DelegateCommand<ApplicationLanguage>(ChangeLanguage);

            InitializeLanguages();
        }

        public ICommand ChangeLanguageCommand { get; private set; }

        public ObservableCollection<ApplicationLanguage> ManifestLanguages { get; set; }

        public ApplicationLanguage SelectedLanguage { get; set; }

        public bool IsRestartRequired
        {
            get { return isRestartRequired; }
            set { SetProperty(ref isRestartRequired, value); }
        }

        private void InitializeLanguages()
        {
            var languages = new List<ApplicationLanguage>();

            foreach (var langTag in ApplicationLanguages.ManifestLanguages)
            {
                var lang = new Windows.Globalization.Language(langTag);
                var name = (lang.NativeName == lang.DisplayName) ? lang.DisplayName : lang.DisplayName + " - " + lang.NativeName;

                languages.Add(new ApplicationLanguage { Tag = langTag, DisplayName = name });
            }

            ManifestLanguages = new ObservableCollection<ApplicationLanguage>(languages);
            SelectedLanguage = ManifestLanguages.FirstOrDefault(x => x.Tag == ApplicationLanguages.PrimaryLanguageOverride);
        }

        private void ChangeLanguage(ApplicationLanguage selectedLanguage)
        {
            IsRestartRequired = initialLanguage != selectedLanguage.Tag;

            ApplicationLanguages.PrimaryLanguageOverride = selectedLanguage.Tag;
        }

        public Action CloseFlyout
        {
            get { return closeFlyout; }
            set { SetProperty(ref closeFlyout, value); }
        }
    }
}

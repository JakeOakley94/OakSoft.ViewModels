using OakSoft.Model;
using OakSoftCore;
using OakSoftCore.Mapping;
using OakSoftCore.MVVM;
using System.Collections.ObjectModel;

namespace OakSoft.ViewModels
{
    public class DocumentViewModel : WrapperViewModelBase<Document>
    {
        private string _fileName;
        public string FileName { get => _fileName; set => TryToSetFieldToNewValue(ref _fileName, value); }

        private string _description;
        public string Description { get => _description; set => TryToSetFieldToNewValue(ref _description, value); }

        private DateTime _documentDate;
        public DateTime DocumentDate { get => _documentDate; set => TryToSetFieldToNewValue(ref _documentDate, value); }

        private int _documentCategoryId;
        public int DocumentCategoryId { get => _documentCategoryId; set => TryToSetFieldToNewValue(ref _documentCategoryId, value); }

        private int _documentSubCategoryId;
        public int DocumentSubCategoryId { get => _documentSubCategoryId; set => TryToSetFieldToNewValue(ref _documentSubCategoryId, value); }

        private byte[] _bytes;
        public byte[] Bytes { get => _bytes; set => TryToSetFieldToNewValue(ref _bytes, value); }

        public ObservableCollection<SelectableItemViewModel> SelectableDocumentCategories { get; set; }

        private SelectableItemViewModel _selectedDocumentCategory;
        public SelectableItemViewModel SelectedDocumentCategory { get => _selectedDocumentCategory; set => TryToSetFieldToNewValue(ref _selectedDocumentCategory, value); }
        public ObservableCollection<SelectableItemViewModel> SelectableDocumentSubCategories { get; set; }

        private SelectableItemViewModel _selectedDocumentSubCategory;
        public SelectableItemViewModel SelectedDocumentSubCategory { get => _selectedDocumentCategory; set => TryToSetFieldToNewValue(ref _selectedDocumentSubCategory, value); }

        public DocumentViewModel()
        {
            SelectableDocumentCategories = new ObservableCollection<SelectableItemViewModel>();
            SelectableDocumentSubCategories = new ObservableCollection<SelectableItemViewModel>();
            PopulateSubItems();
        }

        #region Sub Items

        private void PopulateSubItems()
        {
            PopulateCategories();
        }

        private void PopulateCategories()
        {
            SelectableDocumentCategories = new ObservableCollection<SelectableItemViewModel>();

            foreach (var category in Enum.GetValues(typeof(DocumentCategory)))
            {
                SelectableDocumentCategories.Add(new SelectableItemViewModel((int)category, category.ToString()));
            }

            SelectableDocumentCategories = new ObservableCollection<SelectableItemViewModel>(SelectableDocumentCategories.OrderBy(x => x.Name));
#pragma warning disable CS8601 // Possible null reference assignment.
            SelectedDocumentCategory = SelectableDocumentCategories.SingleOrDefault(x => x.IntId == DocumentCategoryId);
#pragma warning restore CS8601 // Possible null reference assignment.
            Notify(nameof(SelectableDocumentCategories));
        }

        private void UpdateSubCategories()
        {
            if (SelectedDocumentCategory is null) return;

            SelectableDocumentSubCategories.Clear();

            switch (SelectedDocumentCategory.IntId)
            {
                case 1:
                    PopulateHouseCategories();
                    break;
                case 2:
                    PopulateCarCategories();
                    break;
                case 3:
                    PopulatePersonalCategories();
                    break;
                case 4:
                    PopulateHealthcareCategories();
                    break;
                case 5:
                    PopulateInvestmentCategories();
                    break;
                case 6:
                    PopulateShoppingCategories();
                    break;
                case 7:
                    PopulateLegalCategories();
                    break;

                default:
                    break;
            }
        }

        private void PopulateHouseCategories()
        {
            foreach (var category in Enum.GetValues(typeof(HouseDocumentType)))
            {
                SelectableDocumentSubCategories.Add(new SelectableItemViewModel((int)category, category.ToString()));
            }

            SelectableDocumentSubCategories = new ObservableCollection<SelectableItemViewModel>(SelectableDocumentCategories.OrderBy(x => x.Name));
            Notify(nameof(SelectableDocumentSubCategories));
        }

        private void PopulateCarCategories()
        {
            foreach (var category in Enum.GetValues(typeof(CarDocumentType)))
            {
                SelectableDocumentSubCategories.Add(new SelectableItemViewModel((int)category, category.ToString()));
            }

            SelectableDocumentSubCategories = new ObservableCollection<SelectableItemViewModel>(SelectableDocumentCategories.OrderBy(x => x.Name));
            Notify(nameof(SelectableDocumentSubCategories));
        }

        public void OpenDocument()
        {
            throw new NotImplementedException();
        }

        private void PopulatePersonalCategories()
        {
            foreach (var category in Enum.GetValues(typeof(PersonalDocumentType)))
            {
                SelectableDocumentSubCategories.Add(new SelectableItemViewModel((int)category, category.ToString()));
            }

            SelectableDocumentSubCategories = new ObservableCollection<SelectableItemViewModel>(SelectableDocumentCategories.OrderBy(x => x.Name));
            Notify(nameof(SelectableDocumentSubCategories));
        }

        private void PopulateHealthcareCategories()
        {
            foreach (var category in Enum.GetValues(typeof(HealthcareDocumentType)))
            {
                SelectableDocumentSubCategories.Add(new SelectableItemViewModel((int)category, category.ToString()));
            }

            SelectableDocumentSubCategories = new ObservableCollection<SelectableItemViewModel>(SelectableDocumentCategories.OrderBy(x => x.Name));
            Notify(nameof(SelectableDocumentSubCategories));
        }

        private void PopulateInvestmentCategories()
        {
            foreach (var category in Enum.GetValues(typeof(InvestmentDocumentType)))
            {
                SelectableDocumentSubCategories.Add(new SelectableItemViewModel((int)category, category.ToString()));
            }

            SelectableDocumentSubCategories = new ObservableCollection<SelectableItemViewModel>(SelectableDocumentCategories.OrderBy(x => x.Name));
            Notify(nameof(SelectableDocumentSubCategories));
        }

        private void PopulateShoppingCategories()
        {
            foreach (var category in Enum.GetValues(typeof(ShoppingDocumentType)))
            {
                SelectableDocumentSubCategories.Add(new SelectableItemViewModel((int)category, category.ToString()));
            }

            SelectableDocumentSubCategories = new ObservableCollection<SelectableItemViewModel>(SelectableDocumentCategories.OrderBy(x => x.Name));
            Notify(nameof(SelectableDocumentSubCategories));
        }

        private void PopulateLegalCategories()
        {
            foreach (var category in Enum.GetValues(typeof(LegalDocumentType)))
            {
                SelectableDocumentSubCategories.Add(new SelectableItemViewModel((int)category, category.ToString()));
            }

            SelectableDocumentSubCategories = new ObservableCollection<SelectableItemViewModel>(SelectableDocumentCategories.OrderBy(x => x.Name));
            Notify(nameof(SelectableDocumentSubCategories));
        }
        #endregion

        public void OpenDocument(string temporaryDirectory) => FileName.OpenFile(temporaryDirectory, Bytes);

        public void ClearBytes() => Bytes = new byte[0];

        internal void PostMapping(Document src)
        {
            //CreateDateUtc = src.CreateDateUtc.ToLocalTime();
            //UpdateDateUtc = src.UpdateDateUtc.HasValue ? src.UpdateDateUtc.Value.ToLocalTime() : null;
            //DeactivationDateUtc = src.DeactivationDateUtc.HasValue ? src.DeactivationDateUtc.Value.ToLocalTime() : null;
        }

        public override Document BuildModel(OakSoftMapper mapper) => mapper.MapTo<Document>(this);
    }
}

using AutoMapper;
using OakSoft.Services;
using OakSoftCore;
using OakSoftCore.DataAccess.EntityFramework;
using OakSoftCore.Logging;
using OakSoftCore.Mapping;
using OakSoftCore.MVVM;
using System.Collections.ObjectModel;

namespace OakSoft.ViewModels
{
    public abstract class EditorControlViewModelBase<Wrapper, Entity> : ViewModelBase where Wrapper : WrapperViewModelBase<Entity>, new() where Entity : EntityBase, new()
    {
        private string _saveErrorString = string.Empty;
        public List<Guid> _itemsToDeactivate = new List<Guid>();

        private Wrapper _selectedItem;
        public Wrapper SelectedItem { get => _selectedItem; set => TryToSetFieldToNewValue(ref _selectedItem, value); }

        private ILogger _logger;
        public ILogger Logger { get => _logger; set => _logger = value; }

        private OakSoftMapper _mapper;
        public OakSoftMapper Mapper { get => _mapper; private set => _mapper = value; }

        public ObservableCollection<Wrapper> Items { get; set; }

        public EditorControlViewModelBase(OakSoftMapper mapper, ILogger logger)
        {
            _mapper = mapper;
            _logger = logger;
        }

        public abstract RepositoryService<Entity> GetRepositoryService();

        public abstract List<Entity> GetCacheCollection();

        public abstract void PopulateItems();

        public virtual bool Save()
        {
            var repositoryService = GetRepositoryService();

            var createResults = new List<bool>();
            var updateResults = new List<bool>();
            var deactivationResults = new List<bool>();

            foreach (var item in Items.Where(x => x.Edited))
            {
                var result = false;
                var model = item.BuildModel(Mapper);

                if (item.IsNew)
                {
                    result = repositoryService.Create(model);

                    if (result is true)
                    {
                        GetCacheCollection().Add(model);
                        item.Edited = result is false;

                    }
                    createResults.Add(result);
                }
                else
                {
                    result = repositoryService.Update(item.BuildModel(_mapper));
                    item.Edited = result is false;
                    updateResults.Add(result);
                }

                item.Edited = result is false;
            }

            foreach (var itemToDeactivate in _itemsToDeactivate)
            {
                var result = repositoryService.Deactivate(itemToDeactivate);

                if (result is not false)
                {
                    deactivationResults.Add(result);
                    var cacheCollection = GetCacheCollection();
                    var toRemove = cacheCollection.FirstOrDefault(x => x.Id == itemToDeactivate);
                    cacheCollection.Remove(toRemove);
                }

                deactivationResults.Add(result);
            }

            UpdateCacheWithSuccessfulItems();
            return AnalyzeResultsAndOutput(createResults, updateResults, deactivationResults);
        }

        public void UpdateCacheWithSuccessfulItems()
        {

        }

        private bool AnalyzeResultsAndOutput(List<bool> createResults, List<bool> updateResults, List<bool> deactivationResults)
        {
            var hasFailed = createResults.Any(x => x is false)
              || updateResults.Any(x => x is false)
              || deactivationResults.Any(x => x is false);

            _saveErrorString = string.Empty;

            if (hasFailed is true)
            {
                if (createResults.Any(x => x is false))
                {
                    _saveErrorString += $"Failed to create: {createResults.Where(x => x is false).Count()} items. {Environment.NewLine}";
                }

                if (updateResults.Any(x => x is false))
                {
                    _saveErrorString += $"Failed to update: {updateResults.Where(x => x is false).Count()} items. {Environment.NewLine}";
                }

                if (deactivationResults.Any(x => x is false))
                {
                    _saveErrorString += $"Failed to delete: {deactivationResults.Where(x => x is false).Count()} items. {Environment.NewLine}";
                }

                if (string.IsNullOrEmpty(_saveErrorString) is false)
                {
                    _saveErrorString += $"Please check the application log at: {_logger.LogPath} for more information.";
                }
            }

            return hasFailed;
        }

        public bool ShouldClose(DialogResult shouldSave)
        {
            if (UnsavedChangesExist())
            {
                switch (shouldSave)
                {
                    case DialogResult.Yes:
                        return Save();
                    case DialogResult.No:
                        return true;
                    case DialogResult.ClosedWithoutInput:
                        return false;
                    default:
                        return false;
                }
            }

            return true;
        }

        public virtual void AddItem()
        {
            Items.Add(new Wrapper());
            Notify(nameof(Items));
        }

        public void DeactivateItem()
        {
            if (SelectedItem is not null)
            {
                if (Items.Any(x => x.Id == SelectedItem.Id))
                {
                    _itemsToDeactivate.Add(SelectedItem.Id);
                    Items.Remove(Items.Single(x => x.Id == SelectedItem.Id));
                }
            }
        }

        public virtual bool UnsavedChangesExist() => Items.Any(x => x.Edited is true) || (_itemsToDeactivate.Count() > 0);
    }
}

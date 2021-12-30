using SmartGas.Models;
using Xamarin.Forms.Internals;

namespace SmartGas.Controls
{
    /// <summary>
    /// This class extends the behavior of the SearchableListView control to filter the ListViewItem based on text.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class SearchableDocumentsList : SearchableListView
    {
        #region Method

        /// <summary>
        /// Filtering the list view items based on the search text.
        /// </summary>
        /// <param name="obj">The list view item</param>
        /// <returns>Returns the filtered item</returns>
        public override bool FilterContacts(object obj)
        {
            if (base.FilterContacts(obj))
            {
                var taskInfo = obj as PutInModel;
                if (taskInfo == null || string.IsNullOrEmpty(taskInfo.SPARE_PART_CODE) || string.IsNullOrEmpty(taskInfo.NAME))
                {
                    return false;
                }

                return taskInfo.NAME.ToUpperInvariant().Contains(this.SearchText.ToUpperInvariant()) ||
                    taskInfo.SPARE_PART_CODE.ToUpperInvariant().Contains(this.SearchText.ToUpperInvariant());
            }

            return false;
        }

        #endregion
    }
}
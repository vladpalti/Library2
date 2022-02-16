using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Library.Data;

namespace Library.Models
{
    public class BookCategoriesPageModel:PageModel
    {
        public List<AssignedCategoryData> AssignedCategoryDataList;
        public void PopulateAssignedCategoryData(LibraryContext context, Book Book)
        {
            var allCategories = context.Category;
            var BookCategories = new HashSet<int>(Book.BookCategories.Select(c => c.CategoryID)); //
            AssignedCategoryDataList = new List<AssignedCategoryData>();
            foreach (var cat in allCategories)
            {
                AssignedCategoryDataList.Add(new AssignedCategoryData
                {
                    CategoryDataID = cat.CategoryID,
                    Name = cat.CategoryName,
                    Assigned = BookCategories.Contains(cat.CategoryID)
                });
            }
        }

        public void UpdateBookCategories(LibraryContext context, string[] selectedCategories, Book BookToUpdate)
        {
            if (selectedCategories == null)
            {
                BookToUpdate.BookCategories = new List<BookCategory>();
                return;
            }

            var selectedCategoriesHS = new HashSet<string>(selectedCategories);
            var BookCategories = new HashSet<int>
            (BookToUpdate.BookCategories.Select(c => c.Category.CategoryID));
            foreach (var cat in context.Category)
            {
                if (selectedCategoriesHS.Contains(cat.CategoryID.ToString()))
                {
                    if (!BookCategories.Contains(cat.CategoryID))
                    {
                        BookToUpdate.BookCategories.Add(
                        new BookCategory
                        {
                            BookID = BookToUpdate.BookID,
                            CategoryID = cat.CategoryID
                        });
                    }
                }
                else
                {
                    if (BookCategories.Contains(cat.CategoryID))
                    {
                        BookCategory courseToRemove
                        = BookToUpdate
                        .BookCategories
                       .SingleOrDefault(i => i.CategoryID == cat.CategoryID);
                        context.Remove(courseToRemove);
                    }
                }
            }
        }
    }
}

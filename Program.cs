/* 
 *  
 *  
Description: The application uses Entity Framework and LINQ queries to interact with a database 
named "Books" and retrieve specific information. Tasks include fetching lists of titles alongside 
their corresponding authors, ensuring alphabetical sorting, and grouping authors by title with further 
sorting by last name and then first name. 
 * 
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookQuery
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BooksEntities1 dbcontext =
             new BooksEntities1();

            // get authors and titles of each book they co-authored
            // a. Sort the result by title
            var authorsAndTitles =
               from book in dbcontext.Titles
               from author in book.Authors
               orderby book.Title1, author.LastName, author.FirstName
               select new
               {
                   book.Title1,
                   author.LastName,
                   author.FirstName

               };

            Console.WriteLine("\r\n\r\n Titles and Authors:");

            // display titles and authors in tabular format
            // Sort the result by title
            foreach (var element in authorsAndTitles)
            {
                Console.Write(
                   String.Format("\r\n\t{0,-60} {1,-10} {2}",
                    element.Title1, element.FirstName, element.LastName));

            }
            //b.Sort the result by title. For each
            //title sort the authors alphabetically by last name, then first name
            //b.Sort the result by title. For each title sort the authors alphabetically by last name, then first name
            var sortByTitleAndAuthors =
            from book in dbcontext.Titles
            orderby book.Title1
            select new
                {
                    Title = book.Title1,
                    Authors = book.Authors
                                .OrderBy(author => author.LastName)
                                .ThenBy(author => author.FirstName)
                                .Select(author => author.FirstName + " " + author.LastName)
                };

            Console.WriteLine("\r\n\r\n Authors and titles with authors sorted for each title:");

            // display titles and authors in the specified format
            foreach (var titleWithAuthors in sortByTitleAndAuthors)
            {
                foreach (var author in titleWithAuthors.Authors)
                {
                    Console.WriteLine($"{titleWithAuthors.Title} {author}");
                }
            }
            // Part C: Get a list of all the authors grouped by title, sorted by title;
            // for a given title sort the author names alphabetically by last name first then first name
            var authorsGroupedByTitle =
                from book in dbcontext.Titles
                orderby book.Title1
                select new
                {
                    Title = book.Title1,
                    Authors = book.Authors
                                .OrderBy(author => author.LastName)
                                .ThenBy(author => author.FirstName)
                                .Select(author => author.FirstName + " " + author.LastName)
                };
            
            Console.WriteLine("\r\n\r\nTittles grouped by authors, sorted by title:");

            // Display authors grouped by title and sorted as per the requirement
            foreach (var titleWithAuthors in authorsGroupedByTitle)
            {
                Console.WriteLine($"{titleWithAuthors.Title}");
                foreach (var author in titleWithAuthors.Authors)
                {
                    Console.WriteLine($"\t{author}");
                }
            }

            } // end outer foreach
        }
    }



  

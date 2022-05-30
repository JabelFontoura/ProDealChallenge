using AutoMapper;
using ProDealChallenge.Domain.Models;

namespace ProDeal.Application.AutoMapper
{
    public class PathNameResolver
    {
        public string? Resolve(FolderItem source)
        {
            var queue = new Stack<string>();
            var currentParent = source.Parent;
            
            while(currentParent != null)
            {
                queue.Push($"{currentParent.ItemName}/");
                currentParent = currentParent.Parent;
            }

            var result = "";
            while(queue.Count != 0)
            {
                result += queue.Pop();
            }

            return string.IsNullOrEmpty(result) ? null : result + source.ItemName;
        }
    }
}

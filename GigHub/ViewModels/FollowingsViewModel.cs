using GigHub.Models;
using System.Collections.Generic;

namespace GigHub.ViewModels
{
    public class FollowingsViewModel
    {
        public IEnumerable<Following> Followings { get; set; }
        public bool ShowActions { get; set; }
        public string Heading { get; set; }
    }
}
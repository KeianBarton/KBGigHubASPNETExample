using GigHub.Core.Models;
using System.Collections.Generic;

namespace GigHub.Core.Repositories
{
    public interface IGigRepository
    {
        void Add(Gig gig);
        IEnumerable<Gig> GetAllUpcomingGigsWithFollowers();
        Gig GetGig(int gigId);
        IEnumerable<Gig> GetGigsUserAttendingIncludingCancelled(string userId);
        Gig GetGigWithAttendees(int gigId);
        IEnumerable<Gig> GetUpcomingGigsByArtist(string userId);
    }
}

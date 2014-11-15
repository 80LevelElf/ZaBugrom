using System.Web.Mvc;
using Models.Data;
using Models.HelpModels.Element;

namespace ZaBugrom.Controllers
{
    public class ElementController : Controller
    {
        public PartialViewResult PostEditor()
        {
            return PartialView();
        }

        public PartialViewResult RatingVoterForComment(CommentData comment)
        {
            return PartialView("RatingVoter", new VoteRating()
            {
                IsVote = comment.IsVote,
                IsVoteUp = comment.IsVoteUp,
                Rating = comment.Rating
            }); 
        }

        public PartialViewResult RatingVoterForPost(PostData post)
        {
            return PartialView("RatingVoter", new VoteRating()
            {
                IsVote = post.IsVote,
                IsVoteUp = post.IsVoteUp,
                Rating = post.Rating
            }); 
        }

        public PartialViewResult RatingVoter(VoteRating voteRating)
        {
            return PartialView("RatingVoter", voteRating);
        }
    }
}

using System;
namespace ProductReviewsAPI.Models
{
	public class ReviewDTO
	{
		public int Id { get; set; }
		public string Text { get; set; }
		public int Rating { get; set; }

		public ReviewDTO()
		{
		}
	}
}


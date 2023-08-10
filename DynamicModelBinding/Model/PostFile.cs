namespace DynamicModelBinding.Model
{
    public class PostFile
    {
        /// <summary>
        /// Gets or sets the post id.
        /// </summary>
        public int? PostId { get; set; }

        /// <summary>
        /// Gets or sets the post file id.
        /// </summary>
        public int ContentFileId { get; set; }

        /// <summary>
        /// Gets or sets the file blob id.
        /// </summary>
        public Guid BlobId { get; set; }

        /// <summary>
        /// Gets or sets the file name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the content type id.
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// Gets or sets the date the file was created in.
        /// </summary>
        public DateTime DateCreated { get; set; }
    }
}

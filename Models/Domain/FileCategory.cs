using market.Models.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace market.Data.Domain;

public class FileCategory : IEntity
{
    public short Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string SubDirectory { get; set; } = null!;
    public virtual ICollection<File> Files { get; set; } = null!;
}

public class FileCategoryConfiguration : IEntityTypeConfiguration<FileCategory>
{
    public void Configure(EntityTypeBuilder<FileCategory> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Title).IsRequired();
        builder.Property(x => x.Description);
        builder.Property(x => x.SubDirectory);

        builder.HasData(new List<FileCategory>
        {
            new()
            {
                Id = 1,
                Title = "Pictures",
                Description = "All The public pictures, can be hosted in cdn",
                SubDirectory = "pictures"
            },
            new()
            {
                Id = 2,
                Title = "Documents",
                Description =
                    "All kind of documents, like identity or ownership documents. "
                    + "This files are private all the time",
                SubDirectory = "documents"
            }
        });
    }
}
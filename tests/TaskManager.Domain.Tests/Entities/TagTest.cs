using TaskManager.Domain.Entities;
using TaskManager.Domain.Exceptions;

namespace TaskManager.Domain.Tests.Entities;

using Xunit;
using FluentAssertions;

public class TagTest
{

    [Fact]
    public void Constructor_WithValidName_SetsName()
    {
        // Arrange & Act
        Tag t = new Tag("tag name");
        t.Name.Should().Be("tag name");
    }

    [Fact]
    public void Constructor_WhenCalled_GeneratesUniqueId()
    {
        Tag t = new Tag("tag name");
        t.Id.Should().NotBeEmpty();
    }

    [Fact]
    public void Constructor_WithEmptyName_ThrowsException()
    {
        string emptyName = string.Empty;
        Action act = () => new Tag(emptyName);                                                                                               
        act.Should().Throw<EmptyTagNameException>();     
    }

    [Fact]
    public void Rename_WithValidName_UpdatesName()
    {
        Tag t = new Tag("tag name");
        t.UpdateName("new name");
        t.Name.Should().Be("new name");
    }

    [Fact]
    public void Rename_WithEmptyName_ThrowsException()
    {
        Tag t = new Tag("tag name");
        Action act = () => t.UpdateName("");    
        act.Should().Throw<EmptyTagNameException>();     
    }

    [Fact]
    public void ChangeColor_SetsColor()
    {
        Tag t = new Tag("tag name");
        t.ChangeColor("ColorCode");
        t.Color.Should().Be("ColorCode");;
    }
}
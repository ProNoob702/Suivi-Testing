using EventFlow.Core;
using System;

namespace Domain
{
    public readonly struct FileRef : IEquatable<FileRef>, IIdentity
    {
        public Ulid Id { get; }
        public string Value => Id.ToString();

        public FileRef(Ulid id)
        {
            Id = id;
        }

        public FileRef(string id)
        {
            Id = Ulid.Parse(id);
        }

        public static bool operator ==(FileRef left, FileRef right) =>
            Equals(left, right);

        public static bool operator !=(FileRef left, FileRef right) =>
            !Equals(left, right);

        public override bool Equals(object? obj) =>
            obj is FileRef other && Equals(other);

        public bool Equals(FileRef other) =>
            Id == other.Id;

        public override int GetHashCode() =>
            Id.GetHashCode();

        public override string ToString()
        {
            return $"File-{Id}";
        }
    }
}

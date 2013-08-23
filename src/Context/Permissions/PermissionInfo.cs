﻿using Dragon.Interfaces;

namespace Dragon.Context.Permissions
{
    public class PermissionInfo : IPermissionInfo
    {
        public string Subject { get; set; }
        public string Spec { get; set; }
        public bool Inherit { get; set; }
        public bool Inherited { get; set; }

        #region autogenerated

        protected bool Equals(PermissionInfo other)
        {
            return string.Equals(Subject, other.Subject) && string.Equals(Spec, other.Spec) &&
                   Inherit.Equals(other.Inherit) && Inherited.Equals(other.Inherited);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((PermissionInfo) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (Subject != null ? Subject.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Spec != null ? Spec.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ Inherit.GetHashCode();
                hashCode = (hashCode*397) ^ Inherited.GetHashCode();
                return hashCode;
            }
        }

        # endregion
    }
}
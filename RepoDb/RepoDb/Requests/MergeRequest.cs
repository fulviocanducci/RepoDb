﻿using RepoDb.Extensions;
using RepoDb.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;

namespace RepoDb.Requests
{
    /// <summary>
    /// A class that holds the value of the merge operation arguments.
    /// </summary>
    internal class MergeRequest : BaseRequest, IEquatable<MergeRequest>
    {
        private int? m_hashCode = null;

        /// <summary>
        /// Creates a new instance of <see cref="MergeRequest"/> object.
        /// </summary>
        /// <param name="type">The target type.</param>
        /// <param name="connection">The connection object.</param>
        /// <param name="transaction">The transaction object.</param>
        /// <param name="fields">The list of the target fields.</param>
        /// <param name="qualifiers">The list of qualifier <see cref="Field"/> objects.</param>
        /// <param name="statementBuilder">The statement builder.</param>
        public MergeRequest(Type type,
            IDbConnection connection,
            IDbTransaction transaction,
            IEnumerable<Field> fields = null,
            IEnumerable<Field> qualifiers = null,
            IStatementBuilder statementBuilder = null)
            : this(ClassMappedNameCache.Get(type),
                  connection,
                  transaction,
                  fields,
                  qualifiers,
                  statementBuilder)
        {
            Type = type;
        }

        /// <summary>
        /// Creates a new instance of <see cref="MergeRequest"/> object.
        /// </summary>
        /// <param name="name">The name of the request.</param>
        /// <param name="connection">The connection object.</param>
        /// <param name="transaction">The transaction object.</param>
        /// <param name="fields">The list of the target fields.</param>
        /// <param name="qualifiers">The list of qualifier <see cref="Field"/> objects.</param>
        /// <param name="statementBuilder">The statement builder.</param>
        public MergeRequest(string name,
            IDbConnection connection,
            IDbTransaction transaction,
            IEnumerable<Field> fields = null,
            IEnumerable<Field> qualifiers = null,
            IStatementBuilder statementBuilder = null)
            : base(name,
                  connection,
                  transaction,
                  statementBuilder)
        {
            Fields = fields?.AsList();
            Qualifiers = qualifiers?.AsList();
        }

        /// <summary>
        /// Gets the list of the target fields.
        /// </summary>
        public IEnumerable<Field> Fields { get; set; }

        /// <summary>
        /// Gets the qualifier <see cref="Field"/> objects.
        /// </summary>
        public IEnumerable<Field> Qualifiers { get; set; }

        #region Equality and comparers

        /// <summary>
        /// Returns the hashcode for this <see cref="MergeRequest"/>.
        /// </summary>
        /// <returns>The hashcode value.</returns>
        public override int GetHashCode()
        {
            // Make sure to return if it is already provided
            if (m_hashCode != null)
            {
                return m_hashCode.Value;
            }

            // Get first the entity hash code
            var hashCode = string.Concat(Name, ".Merge").GetHashCode();

            // Get the qualifier <see cref="Field"/> objects
            if (Fields != null)
            {
                foreach (var field in Fields)
                {
                    hashCode += field.GetHashCode();
                }
            }

            // Get the qualifier <see cref="Field"/> objects
            if (Qualifiers != null) // Much faster than Qualifers?.<Methods|Properties>
            {
                foreach (var field in Qualifiers)
                {
                    hashCode += field.GetHashCode();
                }
            }

            // Set and return the hashcode
            return (m_hashCode = hashCode).Value;
        }

        /// <summary>
        /// Compares the <see cref="MergeRequest"/> object equality against the given target object.
        /// </summary>
        /// <param name="obj">The object to be compared to the current object.</param>
        /// <returns>True if the instances are equals.</returns>
        public override bool Equals(object obj)
        {
            return obj?.GetHashCode() == GetHashCode();
        }

        /// <summary>
        /// Compares the <see cref="MergeRequest"/> object equality against the given target object.
        /// </summary>
        /// <param name="other">The object to be compared to the current object.</param>
        /// <returns>True if the instances are equal.</returns>
        public bool Equals(MergeRequest other)
        {
            return other?.GetHashCode() == GetHashCode();
        }

        /// <summary>
        /// Compares the equality of the two <see cref="MergeRequest"/> objects.
        /// </summary>
        /// <param name="objA">The first <see cref="MergeRequest"/> object.</param>
        /// <param name="objB">The second <see cref="MergeRequest"/> object.</param>
        /// <returns>True if the instances are equal.</returns>
        public static bool operator ==(MergeRequest objA, MergeRequest objB)
        {
            if (ReferenceEquals(null, objA))
            {
                return ReferenceEquals(null, objB);
            }
            return objB?.GetHashCode() == objA.GetHashCode();
        }

        /// <summary>
        /// Compares the inequality of the two <see cref="MergeRequest"/> objects.
        /// </summary>
        /// <param name="objA">The first <see cref="MergeRequest"/> object.</param>
        /// <param name="objB">The second <see cref="MergeRequest"/> object.</param>
        /// <returns>True if the instances are not equal.</returns>
        public static bool operator !=(MergeRequest objA, MergeRequest objB)
        {
            return (objA == objB) == false;
        }

        #endregion
    }
}

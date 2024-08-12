using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace DreamDazzle.Model
{
    /// <summary>
    /// Base model
    /// </summary>
    public abstract class Base
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Logical delete field
        /// </summary>
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

        /// <summary>
        /// UTC DateTime of last update
        /// </summary>
        public DateTime LastUpdate { get; set; }

        [NotMapped]
        public ActionEnum Action { get; set; }
    }

    public abstract class SPBase
    {
        /// <summary>
        /// Unique identifier
        /// </summary>

        public Guid Id { get; set; }

        /// <summary>
        /// Logical delete field
        /// </summary>
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

        /// <summary>
        /// UTC DateTime of last update
        /// </summary>
        public DateTime LastUpdate { get; set; }

        [NotMapped]
        public ActionEnum Action { get; set; }
    }

    public abstract class BaseDto
    {
        /// <summary>
        /// Unique identifier
        /// </summary>

        public Guid Id { get; set; }

        /// <summary>
        /// Logical delete field
        /// </summary>
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

        /// <summary>
        /// UTC DateTime of last update
        /// </summary>
        public DateTime LastUpdate { get; set; }

        [NotMapped]
        public ActionEnum Action { get; set; }
    }
    public abstract class BaseFilterDto
    {
        public string? SearchQuery { get; set; }
        public string? SearchQueryKey { get; set; }
        public string? SearchQueryValue { get; set; }
        public int? PageCount { get; set; }
        public int? PageNumber { get; set; }
        public string? SortingColumn { get; set; }
        public bool? SortingOrder { get; set; }
        public string? CreatedBy { get; set; }
    }
    public abstract class BaseFilterListDto
    {
        public string? SearchQuerylist { get; set; }
        //public List<SearchQueryDto>? SearchQuerylist { get; set; }
        public string? SearchQuery { get; set; }
        public int? PageCount { get; set; }
        public int? PageNumber { get; set; }
        public string? SortingColumn { get; set; }
        public bool? SortingOrder { get; set; }
        public string? CreatedBy { get; set; }
    }
    public class SearchQuery
    {
        public string? SearchQueryKey { get; set; }
        public string? SearchQueryValue { get; set; }
    }
    public abstract class RecordInfo
    {
        public int? PageSize { get; set; }
        public int? PageNumber { get; set; }
        public int? TotalFilteredRecords { get; set; }
        public int? TotalRecords { get; set; }
    }

}

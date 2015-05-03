using System;

namespace WindowsTasksPlanning.Model
{
    public class ServicePlatform
    {
        private readonly TimeZone _currenTimeZone = System.TimeZone.CurrentTimeZone;
        public virtual long Id { get; set; }

        public virtual int Version { get; set; }

        /// <summary>
        /// The Name of the ServicePlatform
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// The Country of the ServicePlatform
        /// </summary>
        public virtual string Country { get; set; }
        public virtual string ShortDescription { get; set; }
        public virtual DateTime? CreationDate { get; set; }
        public virtual DateTime? ModificationDate { get; set; }
        public virtual string Product { get; set; }

        /// <summary>
        /// The TimeZone property
        /// </summary>
        public virtual string TZ { get; set; }

        /// <summary>
        /// The IP adress property
        /// </summary>
        public virtual string IPAddress { get; set; }
        
        /// <summary>
        /// The data base name
        /// </summary>
        public virtual string DBName { get; set; }
        public virtual string IPAddressType { get; set; }
        public virtual string Login { get; set; }
        public virtual string Password { get; set; }
        
        /// <summary>
        /// The mail adress
        /// </summary>
        public virtual string Contact { get; set; }
        
        /// <summary>
        /// The delay befor allow retry in seconds 
        /// </summary>
        public virtual string Retry_delay { get; set; }

        /// <summary>
        /// The launchAquisitionHour 
        /// </summary>
        public virtual DateTime? LaunchAcquisitionHour { get; set; }
        public virtual DateTime? LaunchAcquisitionMinute { get; set; }
        public virtual DateTime? LastAcquisitionDate { get; set; }
        
        public virtual AcquisitionStatus LastAcquisitionStatus { get; set; }
    }
}

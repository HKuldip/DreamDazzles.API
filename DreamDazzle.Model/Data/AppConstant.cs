using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamDazzle.Model.Data
{
    public class AppConstant
    {
        public const string FailedFetching = "Failed fetching list";
        public const string FailedCreating = "Failed creating";
        public const string FailedUpdating = "Failed Updating";
        public const string FailedDeleting = "Failed Deleting";
        public const string RecordNotFound = "This Record not found";
        public const string ExceptionMsg = "Something Went Wrong, Please connect to Admin";
        public const int ExpirationDays = 7;
        public const string InviteSMS = "Welcome \n, Vist this link to book service with FithooD\n. {0} ";

        public const string FailedPreReservationJob = "Failed creating Job for PreReservation Auto Delete/Cancel.";
        public const string FailedZoomJob = "Failed Creating Job for Zoom TimeSlot.";
        public const string FailedZoomJoinURL = "Failed Creating Zoom JoinURL";
        public const string FailedShortLinkURL = "Failed Creating Short Link URL";

        public const string ServiceNotAssociated = "Service not associated with this timeslotset.";

        public const string FailedPaymentCreating = "Failed creating payment";
        public const string FailedPaymentExecuteCreating = "Failed creating payment execute.";
        public const string ReserveName = "חדרי אימון";
        public const string lang = "he";

        public const string TimeslotCancelled = "Timeslot({0}) already cancelled.";
        public const string NotAssociated = "Inputs Not Associated.";

        public const string DefaultCancellationMessage = "Text for Default Message.";
        public const string FailedPaymenPunchCard = "PunchcardMethodInvalid";

        public const string InValid = "InValid";
        public const string SessionExpired = "SessionExpired";
        public const string NotFound = "Not found";
        public const string LoginFailed = "Not User found";
        public const string LoginSucess = "User Login successfully";
        public const string NoRecords = " No Records";
    }
}

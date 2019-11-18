using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using DICOMcloud.Wado;
using DICOMcloud.Wado.Models;

namespace PACS.Cloud.Controllers
{
    /// <summary>
    /// Query based on ID for DICOM Objects (QIDO) enables you to search for studies, series and instances by patient ID, 
    /// and receive their unique identifiers for further usage (i.e., to retrieve their rendered representations). More detail can be found in PS3.18 6.7.
    /// https://dicomweb.hcintegrations.ca/services/query/
    /// </summary>
    public class QidoRSController : ApiController
    {
        protected IQidoRsService QidoService { get; set; }
        protected IWadoRsService WadoRsService { get; set; }

        public QidoRSController(IQidoRsService qidoService, IWadoRsService wadoRsService)
        {
            QidoService = qidoService;
            WadoRsService = wadoRsService;
        }

        /// <summary>
        /// Look up studies (i.e., for a particular patient)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("wadors/studies")]
        [HttpGet]
        public HttpResponseMessage SearchForStudies
        (
            [ModelBinder(typeof(QidoRequestModelBinder))]
            IQidoRequestModel request
        )
        {
            return QidoService.SearchForStudies(request);
        }

        /// <summary>
        /// Look up a series
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("wadors/studies/{StudyInstanceUID}/series")]
        [Route("wadors/series")]
        [HttpGet]
        public HttpResponseMessage SearchForSeries
        (
            [ModelBinder(typeof(QidoRequestModelBinder))]
            IQidoRequestModel request
        )
        {
            return QidoService.SearchForSeries(request);
        }

        /// <summary>
        /// Look up instances
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("wadors/studies/{StudyInstanceUID}/series/{SeriesInstanceUID}/instances")]
        [Route("wadors/studies/{StudyInstanceUID}/instances")]
        [Route("wadors/instances")]
        [HttpGet]
        public HttpResponseMessage SearchForInstances
        (
            [ModelBinder(typeof(QidoRequestModelBinder))]
            IQidoRequestModel request
        )
        {
            return QidoService.SearchForInstances(request);
        }

        // [Route("qidors/studies/{StudyInstanceUID}/metadata")]
        // [HttpGet]
        // public HttpResponseMessage GetStudiesMetadata
        //(
        //    [ModelBinder(typeof(RsStudiesRequestModelBinder))]
        //     IWadoRsStudiesRequest request
        //)
        // {
        //     return WadoRsService.RetrieveStudyMetadata(request);
        // }
    }
}
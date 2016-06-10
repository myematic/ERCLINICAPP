namespace ERCLINICAPP.Controllers {

    export class HomeController {
        private ClinicsResource;
        private DoctorsResource;
        public clinics;
        public doctors;

        public getClinics() {
            this.clinics = this.ClinicsResource.query();
        }
        public getDoctors() {
            this.doctors = this.DoctorsResource.query();
        }

        constructor(private $resource: angular.resource.IResourceService) {
            this.ClinicsResource = $resource('/api/clinics/:id');
            this.DoctorsResource = $resource('/api/doctors/:id');
            this.getClinics();
            this.getDoctors();
        }
    }


    export class SecretController {
        public secrets;

        constructor($http: ng.IHttpService) {
            $http.get('/api/secrets').then((results) => {
                this.secrets = results.data;
            });
        }
    }


    export class AddClinicController {

        private ClinicResource;
        private AttendingsResource;
        private DoctorsResource;
        public clinic;
        public attendings;
        public doctors;


        public getAttendings() {
            this.attendings = this.AttendingsResource.query();
        }
        public getDoctors() {
            this.doctors = this.DoctorsResource.query();
        }


        public addClinic() {
            this.ClinicResource.save(this.clinic).$promise.then(() => { this.$state.go('home') })

        }

        constructor(private $resource: angular.resource.IResourceService, private $state: ng.ui.IStateService) {
            this.ClinicResource = $resource('/api/clinics/:id');
            this.AttendingsResource = $resource('/api/attendings/:id');
            this.DoctorsResource = $resource('/api/doctors/:id');
            
            this.getAttendings();
            this.getDoctors();
        }
    }
    export class EditClinicController {

    private ClinicResource;
        public clinic;
     
      


      


        public editClinic() {
        this.ClinicResource.save(this.clinic).$promise.then(() => { this.$state.go('home') })

    }

        constructor(private $resource: angular.resource.IResourceService, private $state: ng.ui.IStateService, private $stateParams: ng.ui.IStateParamsService) {
            this.ClinicResource = $resource('/api/clinics/:id');
           
            this.clinic = this.ClinicResource.get({ id: this.$stateParams['id'] })
            //something else here??
           
        }
    }
    export class DeleteClinicController {
    private ClinicResource;
        private AttendingsResource;
        private DoctorsResource;
        public clinic;
        public attendings;
        public doctors;


        public getAttendings() {
        this.attendings = this.AttendingsResource.query();
    }
        public getDoctors() {
        this.doctors = this.DoctorsResource.query();
    }


        public deleteClinic() {
        this.ClinicResource.delete(this.clinic).$promise.then(() => { this.$state.go('home') })

    }

        constructor(private $resource: angular.resource.IResourceService, private $state: ng.ui.IStateService, private $stateParams: ng.ui.IStateParamsService) {
            this.ClinicResource = $resource('/api/clinics/:id');
            this.AttendingsResource = $resource('/api/attendings/:id');
            this.DoctorsResource = $resource('/api/doctors/:id');
            this.clinic = this.ClinicResource.get({ id: this.$stateParams['id'] })
          
            this.getAttendings();
            this.getDoctors();
            
        }
    }
}




















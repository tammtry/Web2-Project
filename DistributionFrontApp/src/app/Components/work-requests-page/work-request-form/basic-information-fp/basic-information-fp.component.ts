import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import * as moment from 'moment';
import { WorkRequestServiceService } from 'src/app/Services/work-request-service.service';
import { customFormValidators } from '../../../../Models/customValidators';

@Component({
  selector: 'app-basic-information-fp',
  templateUrl: './basic-information-fp.component.html',
  styleUrls: ['./basic-information-fp.component.css']
})
export class BasicInformationFPComponent implements OnInit {

  infoForm = this.fb.group({
    type: ['Planned work'],
    street: ['', Validators.required],
    startDate: [null, Validators.required],
    endDate: [null, Validators.required],
    emergency: [false, Validators.required],
    company: ['', Validators.required],
    phoneNumber: ['', [Validators.required, Validators.pattern('^[- +0-9]+$')]],
    purpose: ['', Validators.required],
    details: ['', Validators.required],
    notes: ['']
  },
    { //Custom validacija.
      validator: Validators.compose([customFormValidators.dateLessThan('startDate', 'endDate', { 'dateError': true })])
    });

  constructor(private fb: FormBuilder, private wr: WorkRequestServiceService) { }

  ngOnInit(): void {


    if(sessionStorage.getItem("idDoc") !== null){
      this.getAndFill(sessionStorage.getItem("idDoc"));
    }else if (sessionStorage.getItem("infoForm") !== null) {
      let formdata = JSON.parse(sessionStorage.getItem("infoForm"));
      this.infoForm.setValue(formdata);
    }
    this.onValueChanges();
  }

  onValueChanges(): void {
    this.infoForm.valueChanges.subscribe(val => {
      sessionStorage.setItem("infoForm", JSON.stringify(this.infoForm.value));
      sessionStorage.setItem("infoFormValid", JSON.stringify(this.infoForm.valid));
      
    })
  }

  getAndFill(id){

    this.wr.getBasicInformation(id).subscribe(
      res => {
        this.infoForm.get('type').setValue(res["type"]);
        this.infoForm.get('street').setValue(res["street"]);
        this.infoForm.get('startDate').setValue(moment(res["startDate"]).format('YYYY-MM-DDTHH:mm'));
        this.infoForm.get('endDate').setValue(moment(res["endDate"]).format('YYYY-MM-DDTHH:mm'));
        this.infoForm.get('emergency').setValue(res["emergency"]);
        this.infoForm.get('company').setValue(res["company"]);
        this.infoForm.get('phoneNumber').setValue(res["phoneNumber"]);
        this.infoForm.get('purpose').setValue(res["purpose"]);
        this.infoForm.get('details').setValue(res["details"]);
        this.infoForm.get('notes').setValue(res["notes"]);
        this.infoForm.disable();
      }
    );

  }

  get endDateForm() {
    return this.infoForm.get('endDate');
  }

  get phoneNumber(){
    return this.infoForm.get('phoneNumber');
  }

}

import { NotificationComponent } from './notification/notification.component';
import { Component, OnInit } from '@angular/core';
import { clearAllProjections } from 'ol/proj';
import { ToastrService } from 'ngx-toastr';
import { NotificationService } from 'src/app/Services/notifications/notification.service';
import { Notification } from 'src/app/Models/notification.model';
import { LoggedUser } from 'src/app/Models/LoggedUser.model';


@Component({
  selector: 'app-notifications',
  templateUrl: './notifications.component.html',
  styleUrls: ['./notifications.component.css']
})
export class NotificationsComponent implements OnInit {

  filterOptions = ["All notifications", "Unread", "Errors", "Info", "Success", "Warnings"];
  test = 1;
  selectedFilter= "All notifications";
  notificationMessages;
  user: LoggedUser;

  constructor( private toastr: ToastrService, private notificationService: NotificationService ) { }

  ngOnInit(): void {
    this.user = JSON.parse(sessionStorage.getItem('loggedUser'));
    //this.getAllNotifications();
  }
  async getUnreadNotifications() {
    this.user = JSON.parse(sessionStorage.getItem('loggedUser'));
    await this.notificationService.getUnreadNotifications(this.user.username).subscribe(res=>{this.notificationMessages = res});
  }
  async setMarkAsRead() {
    this.user = JSON.parse(sessionStorage.getItem('loggedUser'));
    let formdata = new FormData();
    formdata.append('username', this.user.username);    
    await this.notificationService.setMarkAsRead(formdata).toPromise();
  }
  markAsRead(){
    this.setMarkAsRead();
  }
  async getAllNotifications(){
    this.user = JSON.parse(sessionStorage.getItem('loggedUser'));
   await this.notificationService.GetUserNotifications(this.user.username).subscribe(res => {this.notificationMessages = res});
  }

  onClick(filter:string){
    this.selectedFilter = filter;
    this.notificationMessages = this.filterFunction();
    console.log(this.notificationMessages);
    console.log(this.selectedFilter);
  }

   filterFunction(){
     console.log(this.selectedFilter);
    switch(this.selectedFilter){
      case 'Success': 
      {
         this.getAllNotifications();
       return this.notificationMessages.filter(i => i.type === 'Success');
      }
      case 'Info': 
      {
         this.getAllNotifications();
        return this.notificationMessages.filter(i => i.type === 'Info');
      }
      case 'Errors': 
      {
         this.getAllNotifications();
         console.log('here');
        return this.notificationMessages.filter(i => i.type === 'Error');
      }
      case 'Warnings': 
      {
         this.getAllNotifications();
        return this.notificationMessages.filter(i => i.type === 'Warning');
      }
      case 'Unread': 
      {        
         this.getUnreadNotifications();
        return this.notificationMessages;
      }
      default: 
      {        
         this.getAllNotifications();
        return this.notificationMessages;
      }
    
      
    }
  }
  async clearAll(){
    this.user = JSON.parse(sessionStorage.getItem('loggedUser'));
    let formdata = new FormData();
    formdata.append('username', this.user.username);    
    await this.notificationService.clearAll(formdata)
        .toPromise();
  }
}

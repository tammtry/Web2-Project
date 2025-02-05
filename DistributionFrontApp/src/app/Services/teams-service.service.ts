import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { TeamMember } from '../Models/TeamMember.model';

@Injectable({
  providedIn: 'root'
})
export class TeamsServiceService {

  constructor(private http: HttpClient) { }

  getAllTeams(){
    return this.http.get('http://localhost:24757/UserApp/Teams/getTeams');
  }

  createTeam(formdata: any){
    return this.http.post('http://localhost:24757/UserApp/Teams/addTeam', formdata);
  }

  getAvailableTeamMembers(){
    return this.http.get<TeamMember[]>('http://localhost:24757/UserApp/Teams/getAvailableTeamMembers');
  }

  getTeam(id){
    return this.http.get('http://localhost:24757/UserApp/Teams/getTeam?id=' + id);
  }

  getTeamMembers(id){
    return this.http.get<TeamMember[]>('http://localhost:24757/UserApp/Teams/getMembers?id=' + id);
  }

  deleteTeam(formdata){
    return this.http.post('http://localhost:24757/UserApp/Teams/deleteTeam', formdata);
  }

  editTeam(formdata){
    return this.http.post('http://localhost:24757/UserApp/Teams/updateTeam', formdata);
  }

}

import { UserApiService } from './../../data/user/user-api.service';
import { Component, OnInit } from '@angular/core';
import { UserProfile } from 'src/app/shared/models/user/userProfile';

@Component({
  selector: 'app-admin-users',
  templateUrl: './admin-users.component.html'
})
export class AdminUsersComponent implements OnInit {

  users: UserProfile[];

  constructor(private userService: UserApiService) { }

  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers() {
    this.userService.getAdminUsers().subscribe(users => {
      this.users = users
    })
  }

}

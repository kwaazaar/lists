import { Component, OnInit } from '@angular/core';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'app';
  
  profile: any;

  constructor(public auth: AuthService) {
    auth.handleAuthentication();
  }

  ngOnInit() {
    if (localStorage.getItem('isLoggedIn') === 'true') {
      this.auth.renewTokens();
    }

    if (this.auth.isAuthenticated) {
      this.fetchUserProfile();
    }
  }

  fetchUserProfile(): void {
    if (this.auth.userProfile) {
      this.profile = this.auth.userProfile;
      console.log('Profile attached for nickname: ', this.profile.nickname);
    } else {
      return this.auth.getProfile((err, profile) => {
        this.profile = profile;
        if (profile) {
          console.log('Profile loaded for nickname: ', profile.nickname);
        }
        if (err) {
          console.error('Error while fetching userprofile:', err);
        }
      });
    }

  }
}

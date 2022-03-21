import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SnackBarService } from 'src/app/services/snack-bar.service';
import { environment } from '../../../../environments/environment';

@Component({
  selector: 'app-invite-finish',
  templateUrl: './invite-finish.component.html',
  styleUrls: ['./invite-finish.component.scss'],
})
export class InviteFinishComponent implements OnInit {
  email!: string;

  constructor(
    private route: ActivatedRoute,
    protected httpClient: HttpClient,
    protected snackBarService: SnackBarService
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      this.email = params['email'];
    });

    this.joinToTeam();
  }

  joinToTeam() {
    let url = environment.apiUrl + '/Users/AddToTeam?authorEmail=' + this.email;
    this.httpClient.get(url).subscribe({
      error: () => {
        this.snackBarService.openSnackBar(
          'Unable to add current user to teams'
        );
      },
    });
  }
}

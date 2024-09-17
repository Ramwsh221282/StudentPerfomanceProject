import { Component } from '@angular/core';
import { StudentGroupsFacadeService } from '../../services/student-groups-facade.service';

@Component({
  selector: 'app-page-group',
  templateUrl: './page-group.component.html',
  styleUrl: './page-group.component.scss',
  providers: [StudentGroupsFacadeService],
})
export class PageGroupComponent {
  public constructor() {}
}

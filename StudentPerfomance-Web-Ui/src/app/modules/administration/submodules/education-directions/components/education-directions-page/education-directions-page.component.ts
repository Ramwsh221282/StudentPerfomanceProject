import { Component } from '@angular/core';
import { CreateService } from '../../services/create.service';
import { DeleteService } from '../../services/delete.service';
import { PaginationService } from '../../services/pagination.service';
import { FetchService } from '../../services/fetch.service';
import { UpdateService } from '../../services/update.service';
import { FacadeService } from '../../services/facade.service';

@Component({
  selector: 'app-education-directions-page',
  templateUrl: './education-directions-page.component.html',
  styleUrl: './education-directions-page.component.scss',
  providers: [
    CreateService,
    DeleteService,
    PaginationService,
    FetchService,
    UpdateService,
    FacadeService,
  ],
})
export class EducationDirectionsPageComponent {}

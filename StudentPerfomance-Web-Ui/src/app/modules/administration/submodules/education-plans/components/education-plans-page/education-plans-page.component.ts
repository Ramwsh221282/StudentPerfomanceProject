import { Component } from '@angular/core';
import { CreateService } from '../../services/create.service';
import { DeleteService } from '../../services/delete.service';
import { PaginationService } from '../../services/pagination.service';
import { FetchService } from '../../services/fetch.service';
import { FacadeService } from '../../services/facade.service';

@Component({
  selector: 'app-education-plans-page',
  templateUrl: './education-plans-page.component.html',
  styleUrl: './education-plans-page.component.scss',
  providers: [
    CreateService,
    DeleteService,
    PaginationService,
    FetchService,
    FacadeService,
  ],
})
export class EducationPlansPageComponent {}
import { Component } from '@angular/core';
import { FacadeService } from '../../services/facade.service';

@Component({
  selector: 'app-education-plans-page',
  templateUrl: './education-plans-page.component.html',
  styleUrl: './education-plans-page.component.scss',
  providers: [FacadeService],
})
export class EducationPlansPageComponent {}

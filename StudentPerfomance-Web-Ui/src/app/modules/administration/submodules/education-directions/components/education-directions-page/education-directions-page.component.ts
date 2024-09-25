import { Component } from '@angular/core';
import { FacadeService } from '../../services/facade.service';

@Component({
  selector: 'app-education-directions-page',
  templateUrl: './education-directions-page.component.html',
  styleUrl: './education-directions-page.component.scss',
  providers: [FacadeService],
})
export class EducationDirectionsPageComponent {}

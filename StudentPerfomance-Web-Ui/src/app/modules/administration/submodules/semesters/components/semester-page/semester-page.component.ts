import { Component } from '@angular/core';
import { SemesterFacadeService } from '../../services/semester-facade.service';

@Component({
  selector: 'app-semester-page',
  templateUrl: './semester-page.component.html',
  styleUrl: './semester-page.component.scss',
  providers: [SemesterFacadeService],
})
export class SemesterPageComponent {}

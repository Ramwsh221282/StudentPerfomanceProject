import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AuthService } from '../../../user-page/services/auth.service';
import { AppConfigService } from '../../../../app.config.service';
import { EducationDirection } from '../../../../modules/administration/submodules/education-directions/models/education-direction-interface';
import { DirectionPayloadBuilder } from '../../../../modules/administration/submodules/education-directions/models/contracts/direction-payload-builder';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'any',
})
export class DeleteEducationDirectionService {
  public constructor(
    private readonly _http: HttpClient,
    private readonly _auth: AuthService,
    private readonly _config: AppConfigService,
  ) {}

  public delete(direction: EducationDirection): Observable<EducationDirection> {
    const url = `${this._config.baseApiUri}/api/education-direction`;
    const body = this.buildPayload(direction);
    const headers = this.buildHttpHeaders();
    return this._http.delete<EducationDirection>(url, {
      headers: headers,
      body,
    });
  }

  private buildHttpHeaders(): HttpHeaders {
    return new HttpHeaders().set('token', this._auth.userData.token);
  }

  private buildPayload(direction: EducationDirection): object {
    return {
      query: DirectionPayloadBuilder(direction),
    };
  }
}

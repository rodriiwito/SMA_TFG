import { Pipe, PipeTransform } from '@angular/core';
import { UserType } from 'src/app/services/models/enums/user-type';

@Pipe({
  name: 'userType',
  standalone: true,
})
export class UserTypePipe implements PipeTransform {
  private map: {
    [key in UserType]: string;
  } = {
    [UserType.Personal]: 'Personal',
    [UserType.WorkorStudent]: 'Work or Student',
  };

  transform(value: UserType): string {
    return this.map[value];
  }
}

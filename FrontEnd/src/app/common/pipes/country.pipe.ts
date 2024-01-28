import { Pipe, PipeTransform } from '@angular/core';
import { Country } from '../country/country.model';

@Pipe({
  name: 'countryName',
  standalone: true,
})
export class CountryPipe implements PipeTransform {
  transform(isoCode: string, countryList: Country[]): string {
    return (
      countryList.find((x) => x.code == isoCode)?.name ?? 'Country not found'
    );
  }
}

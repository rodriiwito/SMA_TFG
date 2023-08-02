import { NgModule } from "@angular/core";
import {RouterModule, Routes} from "@angular/router";
import { ObservatoriosComponent } from "./observatorios/observatorios/observatorios.component";

const routes: Routes = [
    {
        path: '',
        component: ObservatoriosComponent
    },
    {
        path: 'observatorios',
        component: ObservatoriosComponent
    }
];
@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule{ }
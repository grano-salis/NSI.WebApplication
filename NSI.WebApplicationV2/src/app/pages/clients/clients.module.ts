import { ClientDetailsComponent } from './ClientDetails/ClientDetailsComponent';
import { ClientsListComponent } from './ClientsList/ClientsListComponent';
import { ClientsService } from '../../services/clients.service';
import { ClientsRoutingModule } from './clients-routing.module';
import { SharedModule } from './../../shared/shared.module';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';

@NgModule({
   imports: [
    CommonModule,
    SharedModule,
	FormsModule,
    ClientsRoutingModule
  ],
  declarations: [ClientsListComponent, ClientDetailsComponent],
  providers: [ClientsService]
})
export class ClientsModule { }

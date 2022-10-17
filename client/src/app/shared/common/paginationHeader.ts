import { HttpParams } from "@angular/common/http";

export class PaginationHeader {
    static getPaginationHeader(pageNumber:number, pageSize:number) {
        //This will take care of adding our params in the queryString.
        let params = new HttpParams();
    
        params = params.append("currentPage", pageNumber.toString());
        params = params.append("itemsPerPage", pageSize.toString());
    
        return params;
      } 
}
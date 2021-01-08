`timescale 1ns / 1ps
//////////////////////////////////////////////////////////////////////////////////
// Company: 
// Engineer: 
// 
// Create Date:    14:04:38 12/30/2020 
// Design Name: 
// Module Name:    top 
// Project Name: 
// Target Devices: 
// Tool versions: 
// Description: 
//
// Dependencies: 
//
// Revision: 
// Revision 0.01 - File Created
// Additional Comments: 
//
//////////////////////////////////////////////////////////////////////////////////
module top(
           // Clocks
           input i_clk_8mhz,
           input i_clk_pps,
           
           // Flash
           output o_qspi_cs_n,
           output o_qspi_sck,
           inout [3:0] io_qspi_dat,
           
           // Peripherals
           input [1:0] i_btn,
           output [3:0] o_led,
           
           // DEPP
           output reg o_depp_wait,
           input i_depp_astb_n,
           input i_depp_dstb_n,
           input i_depp_write_n,
           inout [7:0] io_depp_data,
           
           // Programmable IO. 24 and 25 are NC
           inout [48:1] io_pio);

/*   bram bram (
     .clka(clka), // input clka
     .ena(ena), // input ena
     .wea(wea), // input [0 : 0] wea
     .addra(addra), // input [7 : 0] addra
     .dina(dina), // input [7 : 0] dina
     .douta(douta) // output [7 : 0] douta
   );*/

   initial o_depp_wait = 1'b0;
   
   wire btn_rst = i_btn[0];
   
   reg [7:0] depp_addr;
   reg [7:0] depp_data; // The one and only register
   
   reg [7:0] depp_out;
   reg depp_read_en;
   reg depp_busy;
   reg depp_debug;
   
   assign o_qspi_cs_n = 1'b1;
   assign o_qspi_sck = 1'b0;
   //assign o_led = {i_depp_astb_n, i_depp_dstb_n, i_depp_write_n, o_depp_wait};
   assign o_led = depp_addr;
   
/*   always @(posedge i_clk_pps or posedge btn_rst) begin
      if (btn_rst) begin
         o_led <= 4'b0000;
      end else begin
         o_led <= i_depp_astb_n;
      end
   end*/
   
   assign io_depp_data = depp_read_en ? depp_out : 8'bzzzzzzzz;
   
   always @(posedge i_clk_8mhz) begin
      if (i_depp_astb_n == 1'b0 | i_depp_dstb_n == 1'b0 | i_depp_write_n == 1'b0 | i_btn[1] == 1'b1) begin
         depp_debug <= 1'b1;
      end else if (i_btn[0] == 1'b1) begin
         depp_debug <= 1'b0;
      end
   end
   
   always @(posedge i_clk_8mhz) begin
      if (depp_busy == 1'b1) begin
         if (i_depp_astb_n == 1'b1 & i_depp_dstb_n == 1'b1) begin
            o_depp_wait <= 1'b0;
            depp_read_en <= 1'b0;
            depp_busy <= 1'b0;
         end
      end else if (i_depp_astb_n == 1'b0) begin
         depp_busy <= 1'b1;
         o_depp_wait <= 1'b1;
         if (i_depp_write_n == 1'b0) begin
            depp_addr <= io_depp_data;
         end else begin
            depp_out <= depp_addr;
            depp_read_en <= 1'b1;
         end
      end else if (i_depp_dstb_n == 1'b0) begin
         depp_busy <= 1'b1;
         o_depp_wait <= 1'b1;
         if (i_depp_write_n == 1'b0) begin
            depp_data <= io_depp_data;
         end else begin
            depp_out <= depp_data;
            depp_read_en <= 1'b1;
         end
      end
   end
      
endmodule

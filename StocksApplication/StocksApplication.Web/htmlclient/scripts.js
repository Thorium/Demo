window.onload = function()
{
    //Debug:
    function concatObject(obj) {
      str='';
      for(prop in obj)
      {
        str+=prop + " value :"+ obj[prop]+"\n";
      }
      return(str);
    }


    Date.prototype.formatDDMMYYYYPlus = function(){
         return this.getDate() + "." + (this.getMonth()+1) + "." + this.getFullYear(); 
    }
    Date.prototype.formatDDMMYYYYMinus = function(){
         return this.getDate() + "." + (this.getMonth()-1) + "." + this.getFullYear(); 
    }

    function toyyyymmdd(str){
        nums = str.split('.');
        function prezero(num){
           if(num.length<2)return "0"+num;
           else return num;
        }       
        return nums[2] + prezero(nums[1]) + prezero(nums[0]);
    };

    var now = new Date();
    $('#DateTo').val(now.formatDDMMYYYYPlus());
    $('#DateFrom').val(new Date(now.getFullYear(), now.getMonth(), 1).formatDDMMYYYYPlus());

    $('#submit').click(function() {
        var service = "http://localhost:49624/StocksService.svc/Symbol/";

        var datef = toyyyymmdd($('#DateFrom').val());
        var datet = toyyyymmdd($('#DateTo').val());

        var url = service + $('#symbol').val() + "/" + datef + "/" + datet;

        //alert(url);

        $.ajax({
            url: url,
            type: "GET",
            processData: false,
            contentType: "application/json",
            dataType: "text",
            timeout: 10000,
            success: function(res){
                var data = JSON2.parse(res); //problems with dates...
//                var data = $.parseJSON(res);

                var len = data.length;
                var ihtml = "";
                for(var i=0;i<len;i++){
                    ihtml = ihtml + "Date: " + data[i].Date.formatDDMMYYYYPlus() + " rate: " + data[i].Rate + "<br/>";
                };
                document.getElementById("stockresult").innerHTML = ihtml;

            },
            error: function(res){alert("Communication error...");}
        });
    });
}



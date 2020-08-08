function getQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]);
    return null;
}  






function FileType2Image(type) {

   

    if (type == ".txt") {
        return "/OfficeImage/txt.png";
    }
    if (type == ".audio") {
        return "/OfficeImage/audio.png";
    }
    if (type == ".csv" || type == ".xlsx" || type == ".xls") {
        return "/OfficeImage/csv.png";
    }
    if (type == ".png" || type == ".jpg" || type == ".jpeg") {
        return "/OfficeImage/image.png";
    }
    if (type == ".pdf") {
        return "/OfficeImage/pdf.png";
    }
    if (type == ".ppt" || type == ".pptx") {
        return "/OfficeImage/ppt.png";
    }
    if (type == ".txt") {
        return "/OfficeImage/txt.png";
    }
    if (type == ".video" || type == ".mp4" || type == ".avi" ) {
        return "/OfficeImage/video.png";
    }
    if (type == ".visio") {
        return "/OfficeImage/visio.png";
    }
    if (type == ".doc" || type == ".docx"){
        return "/OfficeImage/word.png";
    }
    if (type == ".zip") {
        return "/OfficeImage/zip.png";
    }


    return "/OfficeImage/unknown.png";



}
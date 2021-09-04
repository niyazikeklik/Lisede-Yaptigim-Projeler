int count=0;
int KaresiniAl(int taban, int kuvvet){
     int sonuc;
    if (count < kuvvet)
    {
        count++;
        sonuc = taban * taban ;
        KaresiniAl(sonuc,kuvvet);
    }
    else
    {
        return sonuc;
    }

}

int main(){
    
}

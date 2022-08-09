<template>
<v-card
    class="mx-auto"
    max-width="650"
    tile
  >
    <v-list>
        <v-list-item
          v-for="(item, i) in items"
          :key="i"
          :to="item.to + (i + 1)"
          router
          exact
        >
          <v-list-item-content>
            <v-list-item-title v-text="(i + 1) + item.title" />
          </v-list-item-content>
        </v-list-item>
      </v-list>
  </v-card>
</template>

<script>
export default {
  name:"DashboardPage",
  data() {
    return {
      items: [
        {
          title: '. En çok gezilen yer/yerler neresidir?',
          to: '/dashboard/Question'
        },
        {
          title: '. Ağustos ayında en çok çalışan rehber/rehberler kimdir/kimlerdir?' ,
          to: '/dashboard/Question'
        },
        {
          title: '. Kadın turistlerin gezdiği yerleri, toplam ziyaret edilme sayılarıyla beraber listeleyin' ,
          to: '/dashboard/Question'
        },
        {
          title: '. İngiltere’den gelip de Kız Kulesi’ni gezen turistler kimlerdir?' ,
          to: '/dashboard/Question'
        },
        {
          title: '. Gezilen yerler hangi yılda kaç defa gezilmiştir?' ,
          to: '/dashboard/Question'
        },
        {
          title: '. 2’den fazla tura rehberlik eden rehberlerin en çok tanıttıkları yerler nelerdir?' ,
          to: '/dashboard/Question'
        },
        {
          title: '. İtalyan turistler en çok nereyi gezmiştir?' ,
          to: '/dashboard/Question'
        },
        {
          title: '. Kapalı Çarşı’yı gezen en yaşlı turist kimdir?' ,
          to: '/dashboard/Question'
        },
        {
          title: '. Yunanistan’dan gelen Finlandiyalı turistin gezdiği yerler nerelerdir?' ,
          to: '/dashboard/Question'
        },
        {
          title: '. Dolmabahçe Sarayı’na en son giden turistler ve rehberi listeleyin.' ,
          to: '/dashboard/Question'
        },
      ],
    }
  }
}
</script>

<style>

</style>
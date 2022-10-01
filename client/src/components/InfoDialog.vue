<template>
  <v-dialog
    v-model="dialog"
    persistent
    max-width="500"
  >
    <v-card>
      <v-card-title class="text-h5">{{header}}</v-card-title>
      <v-card-text>{{text}}</v-card-text>
      <v-card-actions>
        <v-btn
          color="success"
          outlined
          @click="close()"
        >
          Ok
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>
<script>
  export default{
    name:'InfoDialog',
    props:['header', 'text'],
    data(){
      return{
        dialog: false
      }
    },
    methods:{
      close(){
        this.dialog = false
      }
    },
    watch: {
      text: function(val){
        this.dialog = (val=='')?false:true
      }
    }
  }
</script>
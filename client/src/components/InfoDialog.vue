<template>
  <v-dialog
    v-model="dialog"
    persistent
    max-width="500"
  >
    <v-card>
      <v-card-title class="text-h5">
        <slot name="title"></slot>
      </v-card-title>
      <v-card-text>
        <slot name="text"></slot>
      </v-card-text>
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
    props:{
      visible:Boolean
    },
    data(){
      return{
        dialog: false
      }
    },
    methods:{
      close(){
        this.$emit('update:visible', false)
      }
    },
    watch:{
      visible: function(val){
        this.dialog = val
      }
    }
  }
</script>
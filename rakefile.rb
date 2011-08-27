require "fileutils"

def root
  FileUtils.pwd
end

def program_files
  prog_files_x86 = ENV['programfiles(x86)']
  "#{Dir.exist?(prog_files_x86) ? prog_files_x86 : ENV['programfiles']}".gsub(/\\/, '/')
end

def deploy_raconteur
  integration_debug_dir = "#{root}/IDEIntegration/bin/Debug"

  FileUtils.cp_r "#{integration_debug_dir}/IDEIntegration/.", "#{program_files}/Raconteur"
end

def deploy_resharper_plugin
  plugin_dir = "#{ENV['AppData']}/JetBrains/ReSharper/v6.0/vs10.0/Plugins/Raconteur".gsub(/\\/, '/')

  FileUtils.rm_r plugin_dir if Dir.exist? plugin_dir
  FileUtils.mkdir plugin_dir

  FileUtils.cp Dir.glob("#{root}/Resharper/bin/Debug/*Raconteur*.*"), plugin_dir
end

task :default do
  deploy_raconteur
  deploy_resharper_plugin
end